using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services
{
    public class StaffManagementDataService : IStaffManagementDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();

        /// <summary>
        /// 新增员工管理
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreateStaffManagementDatas(IList<StaffManagementData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的员工管理信息！";
                    return false;
                }
                var datas = data.Where(P=>P.StoreId<=0);
                if (datas.Count() > 0)
                {
                    Message = "存在店铺Id为空，无法生成员工管理信息！";
                    return false;
                }
                var item = data.First().ID ;
                ///获取当前店铺所有的员工管理，用于新增前的判断
                var list = _dbContext.StaffManagementDatas.Where(a => a.StoreId == item).ToList(); 
                
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.StaffManagementDatas.AddRange(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新员工管理
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool UpdateStaffManagementData(StaffManagementData data, out string Message)
        {
            Message = "";
            try
            {
                if (data== null)
                {
                    Message = "参数为空，请输入有效的员工管理！";
                    return false;
                }
                if (data.StoreId <= 0)
                {
                    Message = "店铺Id为空，无法更新员工管理！";
                    return false;
                }
                var reten = _dbContext.StaffManagementDatas.Where(a => a.ID == data.ID && a.StoreId == data.StoreId).FirstOrDefault();
                if (reten == null)
                {
                    Message = $" Id{data.ID}对应的员工管理不存在，无法更新！";
                    return false;
                }
                reten.Nickname = data.Nickname;
                reten.Password = data.Password;
                reten.PhoneNum = data.PhoneNum; 
                
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
        /// 获取员工管理
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public IList<StaffManagementData> getStaffManagementDatas(StaffManagementData data, out string Message)
        {
            Message = "";
            List<StaffManagementData> list = new List<StaffManagementData>();
            try
            {
                if (data==null)
                {
                    Message = "参数为空，请输入有效的员工管理！";
                    return list;
                }
                var storeId = data.StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，获取员工管理信息！";
                    return list;
                }
                list = _dbContext.StaffManagementDatas.Where(a => a.StoreId == storeId).ToList();
               
                if (data.ID>0)
                {
                    list = list.Where(a => a.ID == data.ID).ToList();
                }
                if (!string.IsNullOrEmpty(data.Nickname))
                {
                    list = list.Where(a => a.Nickname.Contains(data.Nickname)).ToList();
                }
                if (!string.IsNullOrEmpty(data.Password))
                {
                    list = list.Where(a => a.Password.Contains(data.Password)).ToList();
                }
                if (!string.IsNullOrEmpty(data.PhoneNum))
                {
                    list = list.Where(a => a.Password.Contains(data.PhoneNum)).ToList();
                } 
            }
            catch (Exception e)
            {
                Message = e.Message;
                return list;
            }
            return list;
        }

        public bool DeleteStaffManagementDatas(IList<StaffManagementData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count()<=0)
                {
                    Message = "参数为空，请输入有效的员工管理信息！";
                    return false;
                }
                foreach (var d in data) {
                    var c = _dbContext.StaffManagementDatas.Where(a => a.ID == d.ID && a.StoreId == d.StoreId).Count();
                    if (d.ID <= 0 || d.StoreId <= 0 || c<=0) {
                        Message = Message + "请确认员工管理Id和门店Id是否输入，否则无法删除！";
                    }
                }
                if (!string.IsNullOrEmpty(Message)) {
                    return false;
                }
                _dbContext.StaffManagementDatas.RemoveRange(data);
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
