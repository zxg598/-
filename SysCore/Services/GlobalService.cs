using SysCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SysCore.Services
{
    public class GlobalService
    {

        /// <summary>
        /// 从datetable转化为Dictionary元素的ArrayList
        /// </summary>
        /// <param name="dtb"></param>
        /// <returns></returns>
        public static ArrayList DateTableToDictionaryArrayList(DataTable dtb)
        {
            
            ArrayList dic = new ArrayList();
            foreach (DataRow dr in dtb.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                foreach (DataColumn dc in dtb.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);
            }
            return dic;
        }

        /// <summary>
        /// 使用dynamic根据DataTable的列名自动添加属性并赋值
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns> 
        public static Object DateTableToDynamicArrayList(DataTable dt)
        {
            ArrayList dic = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                dynamic d = new ExpandoObject();
                foreach (DataColumn dc in dt.Columns)
                {
                    (d as ICollection<KeyValuePair<string, object>>).Add(new KeyValuePair<string, object>(dc.ColumnName, dr[dc.ColumnName]));
                }
                dic.Add(d);
            }
            return dic;
        }

        /// <summary>
        /// DataTable转成List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt)
        {
            var list = new List<T>();
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                object v = null;
                                if (info.PropertyType.ToString().Contains("System.Nullable"))
                                {
                                    v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                                }
                                else
                                {
                                    v = Convert.ChangeType(item[i], info.PropertyType);
                                }
                                info.SetValue(s, v, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }

        /// <summary>
        /// 扩展方法：将List<T>转化为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable datatable = new DataTable();
            PropertyInfo[] propInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var item in propInfo)
            {
                datatable.Columns.Add(item.Name);
            }
            foreach (T item in list)
            {
                var values = new object[propInfo.Length];
                for (int i = 0; i < propInfo.Length; i++)
                {
                    values[i] = propInfo[i].GetValue(item, null);
                }
                datatable.Rows.Add(values);
            }
            return datatable;
        }

    }
}
