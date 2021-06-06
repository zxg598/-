using App.Data;
using App.Models;
using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace App.Services
{
    public class SwitchSettingsDataService : ISwitchSettingsDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();

        /// <summary>
        /// 新增开关设置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreateSwitchSettingsDatas(IList<SwitchSettingsData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的开关设置信息！";
                    return false;
                }
                var storeId = data[0].StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法生成开关设置信息！";
                    return false;
                }   
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.SwitchSettingsDatas.AddRange(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新开关设置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool UpdateSwitchSettingsData(SwitchSettingsData data, out string Message)
        {
            Message = "";
            try
            {
                if (data== null)
                {
                    Message = "参数为空，请输入有效的开关设置信息！";
                    return false;
                }
                if (data.StoreId <= 0)
                {
                    Message = "店铺Id为空，无法更新开关设置信息！";
                    return false;
                } 
                var setting = _dbContext.SwitchSettingsDatas.Where(a => a.StoreId == data.StoreId && a.ID == data.ID).FirstOrDefault();
                setting.ArrivalDeliveryType = data.ArrivalDeliveryType;
                setting.InputSendMessageType = data.InputSendMessageType;
                setting.NotificationType = data.NotificationType;
                setting.SMSpickupType = data.SMSpickupType;
                setting.TakeRemindType = data.TakeRemindType;
                setting.SMSSendingFailedType = data.SMSSendingFailedType;
                setting.NoShelfType = data.NoShelfType;
                setting.NumberDigitsSixType = data.NumberDigitsSixType; 
                setting.SendNotificationType = data.SendNotificationType;
                setting.InTakePhotosType = data.InTakePhotosType;
                setting.ArrivalDeliveryType = data.ArrivalDeliveryType;
                setting.ArrivalSignInType = data.ArrivalSignInType;
                setting.CashOnDeliveryType = data.CashOnDeliveryType;
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
        /// 获取开关设置信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public IList<SwitchSettingsData> getSwitchSettingsDatas(SwitchSettingsData data, out string Message)
        {
            Message = "";
            List<SwitchSettingsData> list = new List<SwitchSettingsData>();
            try
            {
                if (data==null)
                {
                    Message = "参数为空，请输入有效的开关设置信息！";
                    return list;
                }
                var storeId = data.StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法获取开关设置信息！";
                    return list;
                }
                list = _dbContext.SwitchSettingsDatas.Where(a => a.StoreId == storeId).ToList();
                if (data.ID>0) {
                    list = list.Where(a => a.ID == data.ID).ToList();
                }
                if (data.StoreId>0)
                {
                    list = list.Where(a => a.StoreId == data.StoreId).ToList();
                }
                if (data.ArrivalDeliveryType>0)
                {
                    list = list.Where(a => a.ArrivalDeliveryType == data.ArrivalDeliveryType).ToList();
                }
                if (data.ArrivalSignInType>0)
                {
                    list = list.Where(a => a.ArrivalSignInType == data.ArrivalSignInType).ToList();
                }
                if (data.CashOnDeliveryType >=0)
                {
                    list = list.Where(a => a.CashOnDeliveryType == data.CashOnDeliveryType).ToList();
                }
                if (data.InputSendMessageType>0)
                {
                    list = list.Where(a => a.InputSendMessageType == data.InputSendMessageType).ToList();
                } 
                if (data.InTakePhotosType >= 0)
                {
                    list = list.Where(a => a.InTakePhotosType == data.InTakePhotosType).ToList();
                }
                if (data.NoShelfType > 0)
                {
                    list = list.Where(a => a.NoShelfType == data.NoShelfType).ToList();
                }

                if (data.NotificationType >= 0)
                {
                    list = list.Where(a => a.NotificationType == data.NotificationType).ToList();
                }
                if (data.NumberDigitsSixType > 0)
                {
                    list = list.Where(a => a.NumberDigitsSixType == data.NumberDigitsSixType).ToList();
                }

                if (data.SendNotificationType >= 0)
                {
                    list = list.Where(a => a.SendNotificationType == data.SendNotificationType).ToList();
                }
                if (data.SMSpickupType > 0)
                {
                    list = list.Where(a => a.SMSpickupType == data.SMSpickupType).ToList();
                }

                if (data.SMSSendingFailedType >= 0)
                {
                    list = list.Where(a => a.SMSSendingFailedType == data.SMSSendingFailedType).ToList();
                }
                if (data.TakeRemindType > 0)
                {
                    list = list.Where(a => a.TakeRemindType == data.TakeRemindType).ToList();
                }
            }
            catch (Exception e)
            {
                Message = e.Message;
                return list;
            }
            return list;
        }

        public bool DeleteSwitchSettingsDatas(IList<SwitchSettingsData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count()<=0)
                {
                    Message = "参数为空，请输入有效的开关设置信息！";
                    return false;
                }
                foreach (var d in data) {
                    var c = _dbContext.SwitchSettingsDatas.Where(a => a.ID == d.ID && a.StoreId == d.StoreId).Count();
                    if (d.ID <= 0 || d.StoreId <= 0 || c<=0) {
                        Message = Message + "请确认开关设置Id和门店Id是否输入，否则无法删除！";
                    } 
                }
                if (!string.IsNullOrEmpty(Message)) {
                    return false;
                }
                _dbContext.SwitchSettingsDatas.RemoveRange(data);
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
