using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services
{
    /// <summary>
    /// 到站短信模板设置
    /// </summary>
    public class ArrivalSMSDataService : IArrivalSMSDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();

        /// <summary>
        /// 新增到站短信模板设置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreateArrivalSMSDatas(IList<ArrivalSMSData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的到站短信模板！";
                    return false;
                }
                var storeId = data[0].StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法生成到站短信模板！";
                    return false;
                }
                ///获取当前店铺所有的模板信息，用于新增前的判断
                var list = _dbContext.ArrivalSMSDatas.Where(a => a.StoreId == storeId).ToList();
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.ArrivalSMSDatas.AddRange(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新短信模板
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool UpdateArrivalSMSDatas(ArrivalSMSData data, out string Message)
        {
            Message = "";
            try
            {
                if (data== null)
                {
                    Message = "参数为空，请输入有效的短信模板信息！";
                    return false;
                }
                if (data.StoreId <= 0)
                {
                    Message = "店铺Id为空，无法更新短信模板信息！";
                    return false;
                }
                ///获取当前店铺所有的短信模板，用于更新前的判断
                var num = _dbContext.ArrivalSMSDatas.Where(a => a.StoreId == data.StoreId && a.Name==data.Name && a.ID!=data.ID).Count();
                if (num > 0) {
                    Message = $" 短信模板名称信息已存在，请更换名字！";
                    return false;
                }
                var cour = _dbContext.ArrivalSMSDatas.Where(a => a.StoreId == data.StoreId &&  a.ID == data.ID).FirstOrDefault();
                if (cour == null) {
                    Message = $" ID {data.ID}对应的模板名称信息不存在，无法更新！";
                    return false;
                }
             
                if (data.SMSStatus > 0) {
                    cour.SMSStatus = data.SMSStatus;
                }
                if (data.SMSType > 0) {
                    cour.SMSType = data.SMSType;
                }
                cour.Name = data.Name;
                cour.SMSTime = data.SMSTime;
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
        /// 获取短信模板
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public IList<ArrivalSMSData> getArrivalSMSDatas(ArrivalSMSData data, out string Message)
        {
            Message = "";
            List<ArrivalSMSData> list = new List<ArrivalSMSData>();
            try
            {
                if (data==null)
                {
                    Message = "参数为空，请输入有效的短信模板！";
                    return list;
                }
                var storeId = data.StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，不能获取短信模板信息！";
                    return list;
                }
                list = _dbContext.ArrivalSMSDatas.Where(a => a.StoreId == storeId).ToList();
                if (data.ID>0)
                {
                    list = list.Where(a => a.ID == data.ID).ToList();
                } 
                if (data.StoreId > 0)
                {
                    list = list.Where(a => a.StoreId == data.StoreId).ToList();
                }
                if (!string.IsNullOrEmpty( data.Name) )
                {
                    list = list.Where(a => a.Name == data.Name).ToList();
                }
                if (!string.IsNullOrEmpty(data.Details))
                {
                    list = list.Where(a => a.Details.Contains(data.Details)).ToList();
                }
                if (data.SMSTime!=null)
                {
                    list = list.Where(a => a.SMSTime == data.SMSTime).ToList();
                }
                if (data.SMSStatus > 0)
                {
                    list = list.Where(a => a.SMSStatus == data.SMSStatus).ToList();
                }
            }
            catch (Exception e)
            {
                Message = e.Message;
                return list;
            }
            return list;
        }

        public bool DeleteArrivalSMSDatas(IList<ArrivalSMSData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count()<=0)
                {
                    Message = "参数为空，请输入有效的短信模板！";
                    return false;
                } 
                if (!string.IsNullOrEmpty(Message)) {
                    return false;
                }
                _dbContext.ArrivalSMSDatas.RemoveRange(data);
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
