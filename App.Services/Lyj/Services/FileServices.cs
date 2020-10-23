using Aspose.Cells;
using Microsoft.AspNetCore.Http;
using SysCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace App.Services
{
    public class FileServices:IFileService
    {
        /// <summary>
        /// 根据数据源和模板导出excel，返回文件路劲,para表示参数的，可为空
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="dataSource"></param>
        /// <param name="outFileName"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public string ExportExcel(string templatePath, DataTable dataSource, string outFileName,DataTable para=null)
        {
            var ret = outFileName;
            try
            {
                templatePath = templatePath.Replace(@"\", @"/").Replace(@"//", @"/").Replace(@"//", @"/");
                outFileName = outFileName.Replace(@"\", @"/").Replace(@"//", @"/").Replace(@"//", @"/");
                var localRoot = AppContext.BaseDirectory.Replace(@"bin\Debug\netcoreapp3.1", @"");
                templatePath = (localRoot + templatePath).Replace(@"\", @"/").Replace(@"//", @"/").Replace(@"//", @"/");
                outFileName = (localRoot + outFileName).Replace(@"\", @"/").Replace(@"//", @"/").Replace(@"//", @"/");

                Workbook wk = new Workbook(templatePath);
                WorkbookDesigner designer = new WorkbookDesigner(wk);
                DataSet dt = new DataSet();
                dataSource.TableName = "Data";
                dt.Tables.Add(dataSource);
                if (para != null)
                {
                    para.TableName = "Para";
                    dt.Tables.Add(para);
                }
                //else {
                //    para = new DataTable();
                //    para.Columns.Add("Company");
                //    para.Columns.Add("UserName");
                //    DataRow row = para.NewRow();
                //    row["Company"] = "测试";
                //    row["UserName"] = "张三";
                //    para.Rows.Add(row);
                //    para.TableName = "Para";
                //    dt.Tables.Add(para);
                //}
                //designer.SetDataSource(dataSource);
                designer.SetDataSource(dt);
                designer.Process();
                //designer.Workbook.Save(outFileName);
                designer.Workbook.Save(outFileName, SaveFormat.Auto);
                string displayUrl = MyHttpContext.Current.Request.GetAbsoluteUri();
                var rootPath = displayUrl.Substring(0, displayUrl.IndexOf(@"api") - 1);
                ret = (rootPath + ret).Replace(@"\", @"/").Replace(@"//", @"/").Replace(@"//", @"/");
            }
            catch (Exception ex) {
                ret = "";
            }
            return ret;
        }

    }
}