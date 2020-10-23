using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services
{
    public class ExpressStaffDataService : IExpressStaffDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();

        /// <summary>
        /// 新增快递员信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreateExpressStaffDatas(IList<ExpressStaffData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的快递员信息！";
                    return false;
                }
                var storeId = data[0].StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法生成快递员信息！";
                    return false;
                }


                ///获取所有的快递员信息
                var list = _dbContext.ExpressStaffDatas.Where(a => a.StoreId == storeId).ToList();
                var companyList=_dbContext.ExpresscompanyDatas.Where(a=> a.StoreId == storeId).ToList();
                foreach (var d in data)
                {
                    var company = companyList.Where(a => a.ID == d.CompanyId).FirstOrDefault();
                    if (company == null)
                    {
                        Message = Message + $" Id{d.CompanyId} 找不到对应的快递公司，无法新增快递员！";
                    }
                    else {
                        var num = list.Where(a => a.Name == d.Name&&a.CompanyId==d.CompanyId).Count();
                        if (num > 0)
                        {
                            Message = Message + $" 快递员{d.Name}已存在，无法再次新增！";
                        }
                    }
                   
                }
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.ExpressStaffDatas.AddRange(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新快递员信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool UpdateExpressStaffData(ExpressStaffData data, out string Message)
        {
            Message = "";
            try
            {
                if (data== null)
                {
                    Message = "参数为空，请输入有效的快递员信息！";
                    return false;
                }
                if (data.StoreId <= 0)
                {
                    Message = "店铺Id为空，无法更新快递员信息！";
                    return false;
                }

                var user = _dbContext.ExpressStaffDatas.Where(a => a.StoreId == data.StoreId && a.ID == data.ID).FirstOrDefault();

                if (!string.IsNullOrEmpty(data.Name)&&data.Name!=user.Name)
                {
                    ///获取当前店铺下,快递员是否存在
                    var num = _dbContext.ExpressStaffDatas.Where(a => a.StoreId == data.StoreId && a.Name == data.Name && a.ID != data.ID).Count();
                    if (num > 0)
                    {
                        Message = $" 快递员{data.Name}已存在，无法更新！";
                        return false;
                    }
                    user.Name = data.Name;
                }
                if (data.CompanyId > 0 && user.CompanyId != data.CompanyId)
                {
                    var company = _dbContext.ExpresscompanyDatas.Where(a => a.ID == data.CompanyId).FirstOrDefault();
                    if (company == null)
                    {
                        Message = $" 根据Id{data.CompanyId} 找不到对应的公司信息，无法将快递员更新到该公司下！";
                        return false;
                    }
                    user.CompanyId = data.CompanyId;
                }
                if (!string.IsNullOrEmpty(data.Remarks))
                {
                    user.Remarks = data.Remarks;
                }
                if (!string.IsNullOrEmpty(data.PhoneNum))
                {
                    user.PhoneNum = data.PhoneNum;
                }
                if (data.DefaultType>0)
                {
                    user.DefaultType = data.DefaultType;
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
        /// 获取快递员信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public IList<ExpressStaffData> getExpressStaffDatas(ExpressStaffData data, out string Message)
        {
            Message = "";
            List<ExpressStaffData> list = new List<ExpressStaffData>();
            try
            {
                if (data==null)
                {
                    Message = "参数为空，请输入有效的快递员信息！";
                    return list;
                }
                var storeId = data.StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法获取快递员信息！";
                    return list;
                }
                list = _dbContext.ExpressStaffDatas.Where(a => a.StoreId == storeId).ToList();
                if (!string.IsNullOrEmpty(data.Name)) {
                    list = list.Where(a => a.Name == data.Name).ToList();
                }
                if (!string.IsNullOrEmpty(data.Remarks))
                {
                    list = list.Where(a => a.Remarks == data.Remarks).ToList();
                }
                if (!string.IsNullOrEmpty(data.PhoneNum))
                {
                    list = list.Where(a => a.PhoneNum == data.PhoneNum).ToList();
                }
                if (data.CompanyId > 0)
                {
                    list = list.Where(a => a.CompanyId == data.CompanyId).ToList();
                }
                if (data.DefaultType >=0)
                {
                    list = list.Where(a => a.DefaultType == data.DefaultType).ToList();
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

        public bool DeleteExpressStaffDatas(IList<ExpressStaffData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count()<=0)
                {
                    Message = "参数为空，请输入有效的快递员信息！";
                    return false;
                }
                foreach (var d in data) {
                    var c = _dbContext.ExpressStaffDatas.Where(a => a.ID == d.ID && a.StoreId == d.StoreId).Count();
                    if (d.ID <= 0 || d.StoreId <= 0 || c<=0) {
                        Message = Message + "请确认快递员Id和门店Id是否输入，否则无法删除！";
                    }
                    var num = _dbContext.CooperativeCourierDatas.Where(a => a.CourierId == d.ID).Count();
                    if (num>0)
                    {
                        Message = Message + $"在合作快递员数据中，存在该快递员{d.Name}，请先删除对应记录！";
                    }
                }
                if (!string.IsNullOrEmpty(Message)) {
                    return false;
                }
                _dbContext.ExpressStaffDatas.RemoveRange(data);
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
