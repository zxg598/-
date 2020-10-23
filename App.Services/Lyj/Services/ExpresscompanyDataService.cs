using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services
{
    public class ExpresscompanyDataService : IExpresscompanyDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();

        /// <summary>
        /// 新增快递信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreateExpresscompanyDatas(IList<ExpresscompanyData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的快递公司！";
                    return false;
                }
                var storeId = data[0].StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法生成快递公司信息！";
                    return false;
                }
                ///获取当前店铺所有的快递公司，用于新增前的判断
                var list = _dbContext.ExpresscompanyDatas.Where(a => a.StoreId == storeId).ToList();
                foreach (var d in data)
                {
                    var num = list.Where(a => a.Name == d.Name).Count();
                    if (num > 0)
                    {
                        Message = Message + $" 公司{d.Name}已存在，无法再次新增！";
                    }
                }
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.ExpresscompanyDatas.AddRange(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool UpdateExpresscompanyDatas(ExpresscompanyData data, out string Message)
        {
            Message = "";
            try
            {
                if (data == null)
                {
                    Message = "参数为空，请输入有效的快递公司！";
                    return false;
                }
                if (data.StoreId <= 0)
                {
                    Message = "店铺Id为空，无法更新快递公司信息！";
                    return false;
                }
                ///获取当前店铺所有的快递公司，用于更新前的判断
                var num = _dbContext.ExpresscompanyDatas.Where(a => a.StoreId == data.StoreId && a.Name == data.Name && a.ID != data.ID).Count();
                if (num > 0)
                {
                    Message = $" 公司{data.Name}已存在，无法更新公司信息！";
                    return false;
                }
                var company = _dbContext.ExpresscompanyDatas.Where(a => a.StoreId == data.StoreId && a.ID == data.ID).FirstOrDefault();
                if (company == null)
                {
                    Message = $" ID {data.ID}对应的公司信息不存在，无法更新！";
                    return false;
                }
                if (!string.IsNullOrEmpty(data.Name)) {
                    company.Name = data.Name;
                }
                if (!string.IsNullOrEmpty(data.Remarks))
                {
                    company.Remarks = data.Remarks;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public IList<ExpresscompanyData> getExpresscompanyDatas(ExpresscompanyData data, out string Message)
        {
            Message = "";
            List<ExpresscompanyData> list = new List<ExpresscompanyData>();
            try
            {
                if (data==null)
                {
                    Message = "参数为空，请输入有效的快递公司！";
                    return list;
                }
                var storeId = data.StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，获取快递公司信息！";
                    return list;
                }
                list = _dbContext.ExpresscompanyDatas.Where(a => a.StoreId == storeId).ToList();
                if (!string.IsNullOrEmpty(data.Name)) {
                    list = list.Where(a => a.Name == data.Name).ToList();
                }
                if (!string.IsNullOrEmpty(data.Remarks))
                {
                    list = list.Where(a => a.Remarks == data.Remarks).ToList();
                }
                if (data.ID>0)
                {
                    list = list.Where(a => a.ID == data.ID).ToList();
                }
            }
            catch (Exception e)
            {
                Message = e.Message;
                return list;
            }
            return list;
        }

        public bool DeleteExpresscompanyDatas(IList<ExpresscompanyData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count()<=0)
                {
                    Message = "参数为空，请输入有效的快递公司！";
                    return false;
                }
                foreach (var d in data)
                {
                    var num = _dbContext.ExpresscompanyDatas.Where(a => a.ID == d.ID && a.StoreId == d.StoreId).Count();
                    if (d.ID <= 0 || d.StoreId <= 0 || num <=0)
                    {
                        Message = Message + "请确认快递公司Id和门店Id是否输入，否则无法删除！";
                    }
                    var num1 = _dbContext.ExpressStaffDatas.Where(a => a.CompanyId == d.ID).Count();
                    if (num1 > 0)
                    {
                        Message = Message + $"快递员数据中，存在使用过得快递公司{d.Name}的信息，请先删除该条快递员数据！";
                    }
                    var num2 = _dbContext.CooperativeCourierDatas.Where(a => a.ExpressCompanyId == d.ID).Count();
                    if (num1 > 0)
                    {
                        Message = Message + $"合作快递员数据中，存在使用过得快递公司{d.Name}的信息，请先删除该条合作快递员数据！";
                    }
                    var num3 = _dbContext.RetentionTimeDatas.Where(a => a.ExpressCompanyId == d.ID).Count();
                    if (num1 > 0)
                    {
                        Message = Message + $"滞留时间配置信息中，存在使用过得快递公司{d.Name}的信息，请先删除该公司对应的滞留时间配置信息！";
                    }
                }
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.ExpresscompanyDatas.RemoveRange(data);
                _dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
            return true;
        }
    }
}
