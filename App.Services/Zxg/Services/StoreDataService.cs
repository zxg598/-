using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services
{
    public class StoreDataService : IStoreDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();
         
        /// <summary>
        /// 新增店铺信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreateStoreDatas(IList<StoreData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的店铺信息！";
                    return false;
                }
                var storeId = data[0].ID;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法生成店铺信息！";
                    return false;
                }
               
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.StoreDatas.AddRange(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新合作快递员信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool UpdateStoreData(StoreData data, out string Message)
        {
            Message = "";
            try
            {
                if (data== null)
                {
                    Message = "参数为空，请输入有效的店铺信息！";
                    return false;
                }
                if (data.ID <= 0)
                {
                    Message = "店铺Id为空，无法更新店铺信息！";
                    return false;
                }
               
                var cour = _dbContext.StoreDatas.Where(a =>  a.ID == data.ID).FirstOrDefault();
                if (cour == null) {
                    Message = $" ID {data.ID}对应的店铺信息不存在，无法更新！";
                    return false;
                }
                cour.StoreName = data.StoreName;
                cour.PickUpAddress = data.PickUpAddress;
                cour.OpeningTime = data.OpeningTime;
                cour.ClosingTime = data.ClosingTime;
                cour.PhoneNum = data.PhoneNum;
                cour.Area = data.Area;
                cour.Address = data.Address;
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
        /// 获取店铺信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public IList<StoreData> getStoreDatas(StoreData data, out string Message)
        {
            Message = "";
            List<StoreData> list = new List<StoreData>();
            try
            {
                if (data==null)
                {
                    list = _dbContext.StoreDatas.ToList();
                    return list;
                }
               
                if (data.ID>0)
                {
                    list = list.Where(a => a.ID == data.ID).ToList();
                }
                if (!string.IsNullOrEmpty( data.PhoneNum) )
                {
                    list = list.Where(a => a.PhoneNum.Contains( data.PhoneNum)).ToList();
                }
                if (!string.IsNullOrEmpty(data.PickUpAddress))
                {
                    list = list.Where(a => a.PickUpAddress.Contains( data.PickUpAddress)).ToList();
                }
                if (!string.IsNullOrEmpty(data.StoreName))
                {
                    list = list.Where(a => a.StoreName == data.StoreName).ToList();
                }
                if (!string.IsNullOrEmpty(data.Address))
                {
                    list = list.Where(a => a.Address.Contains(data.Address)).ToList();
                }
                if (!string.IsNullOrEmpty(data.Area))
                {
                    list = list.Where(a => a.Area.Contains(data.Area)).ToList();
                }
                if (data.OpeningTime != null)
                {
                    list = list.Where(a => a.OpeningTime==data.OpeningTime).ToList();
                }
                if (data.ClosingTime != null)
                {
                    list = list.Where(a => a.ClosingTime == data.ClosingTime).ToList();
                }
            }
            catch (Exception e)
            {
                Message = e.Message;
                return list;
            }
            return list;
        }

        public bool DeleteStoreDatas(IList<StoreData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count()<=0)
                {
                    Message = "参数为空，请输入有效的店铺信息！";
                    return false;
                }
                foreach (var d in data) {
                    if (d.ID <= 0 ) {
                        Message = Message + "请确认店铺信息Id是否输入，否则无法删除！";
                    }
                }
                if (!string.IsNullOrEmpty(Message)) {
                    return false;
                }
                _dbContext.StoreDatas.RemoveRange(data);
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
