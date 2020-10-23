using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services
{
    public class RetentionTimeDataService : IRetentionTimeDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();

        /// <summary>
        /// 新增滞留时间配置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreateRetentionTimeDatas(IList<RetentionTimeData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的滞留时间配置信息！";
                    return false;
                }
                var storeId = data[0].StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法生成滞留时间配置信息！";
                    return false;
                }
                ///获取当前店铺所有的滞留时间配置信息，用于新增前的判断
                var list = _dbContext.RetentionTimeDatas.Where(a => a.StoreId == storeId).ToList();
                //公司信息
                var company = _dbContext.ExpresscompanyDatas.Where(a => a.StoreId == storeId).ToList();
                foreach (var d in data)
                {
                    var com = company.Where(a => a.ID == d.ExpressCompanyId).FirstOrDefault();
                    if (com == null) {
                        Message = Message + $" 快递公司{com?.Name}不存在，请先维护！";
                    }
                    var num = list.Where(a => a.ExpressCompanyId == d.ExpressCompanyId).Count();
                    if (num > 0)
                    {
                        Message = Message + $" 快递公司{com?.Name}对应的滞留时间配置信息已存在，无法再次新增！";
                    }
                }
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.RetentionTimeDatas.AddRange(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新滞留时间配置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool UpdateRetentionTimeData(RetentionTimeData data, out string Message)
        {
            Message = "";
            try
            {
                if (data== null)
                {
                    Message = "参数为空，请输入有效的滞留配置信息！";
                    return false;
                }
                if (data.StoreId <= 0)
                {
                    Message = "店铺Id为空，无法更新滞留配置信息！";
                    return false;
                }
                var reten = _dbContext.RetentionTimeDatas.Where(a => a.ID == data.ID && a.StoreId == data.StoreId).FirstOrDefault();
                if (reten == null)
                {
                    Message = $" Id{data.ID}对应的滞留信息不存在，无法更新！";
                    return false;
                }
                reten.RemainingPartsDays = data.RemainingPartsDays;

                if (data.ExpressCompanyId > 0) {
                    var company = _dbContext.ExpresscompanyDatas.Where(a => a.ID == data.ExpressCompanyId).FirstOrDefault();
                    if (company == null)
                    {
                        Message = $" ID {data.ExpressCompanyId}对应的快递公司信息不存在，无法更新！";
                        return false;
                    }

                    ///获取当前店铺所有的快递公司，用于更新前的判断
                    var num = _dbContext.RetentionTimeDatas.Where(a => a.StoreId == data.StoreId && a.ExpressCompanyId == data.ExpressCompanyId && a.ID != data.ID).Count();
                    if (num > 0)
                    {
                        Message = $" 快递公司{company?.Name}对应的滞留信息已存在，无法将该滞留信息挂在{company?.Name}下！";
                        return false;
                    }
                    reten.ExpressCompanyId = data.ExpressCompanyId;
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
        public IList<RetentionTimeData> getRetentionTimeDatas(RetentionTimeData data, out string Message)
        {
            Message = "";
            List<RetentionTimeData> list = new List<RetentionTimeData>();
            try
            {
                if (data==null)
                {
                    Message = "参数为空，请输入有效的滞留时间配置信息！";
                    return list;
                }
                var storeId = data.StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，获取滞留时间配置信息！";
                    return list;
                }
                list = _dbContext.RetentionTimeDatas.Where(a => a.StoreId == storeId).ToList();
               
                if (data.ID>0)
                {
                    list = list.Where(a => a.ID == data.ID).ToList();
                }
                if (data.ExpressCompanyId > 0)
                {
                    list = list.Where(a => a.ExpressCompanyId == data.ExpressCompanyId).ToList();
                }
                if (data.RemainingPartsDays > 0)
                {
                    list = list.Where(a => a.RemainingPartsDays == data.RemainingPartsDays).ToList();
                }
            }
            catch (Exception e)
            {
                Message = e.Message;
                return list;
            }
            return list;
        }

        public bool DeleteRetentionTimeDatas(IList<RetentionTimeData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count()<=0)
                {
                    Message = "参数为空，请输入有效的滞留时间配置信息！";
                    return false;
                }
                foreach (var d in data) {
                    var c = _dbContext.RetentionTimeDatas.Where(a => a.ID == d.ID && a.StoreId == d.StoreId).Count();
                    if (d.ID <= 0 || d.StoreId <= 0 || c<=0) {
                        Message = Message + "请确认滞留时间配置信息Id和门店Id是否输入，否则无法删除！";
                    }
                }
                if (!string.IsNullOrEmpty(Message)) {
                    return false;
                }
                _dbContext.RetentionTimeDatas.RemoveRange(data);
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
