using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services
{
    public class CooperativeCourierDataService : ICooperativeCourierDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();
         
        /// <summary>
        /// 新增合作快递员信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreateCooperativeCourierDatas(IList<CooperativeCourierData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的合作快递员信息！";
                    return false;
                }
                var storeId = data[0].StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法生成合作快递员信息！";
                    return false;
                }
                ///获取当前店铺所有的合作快递员信息，用于新增前的判断
                var list = _dbContext.CooperativeCourierDatas.Where(a => a.StoreId == storeId).ToList();
                //获取所有的快递公司信息
                var companyList = _dbContext.ExpresscompanyDatas.Where(a => a.StoreId == storeId).ToList();
                //获取所有的快递员信息
                var expressList = _dbContext.ExpressStaffDatas.Where(a => a.StoreId == storeId).ToList();
                foreach (var d in data)
                {
                    var num1 = companyList.Where(a => a.ID == d.ExpressCompanyId).Count();
                    if (num1 <= 0)
                    {
                        Message = Message + $" 根据Id:{d.ExpressCompanyId}找不到对应的快递公司，无法新增合作快递员信息！";
                    }
                    else if (expressList.Where(a => a.ID == d.CourierId).Count() <= 0)
                    {
                        Message = Message + $" 根据Id:{d.CourierId}找不到对应的快递员，无法新增合作快递员信息！";
                    }
                    else if (expressList.Where(a => a.ID == d.CourierId && a.CompanyId == d.ExpressCompanyId).Count() <= 0)
                    {
                        Message = Message + $" 快递员(ID:{d.CourierId})非该快递公司(Id:{d.ExpressCompanyId})的，无法添加！";
                    }
                    else if (list.Where(a => a.CourierId == d.CourierId && a.ID != d.ID).Count() > 0) {
                        Message = Message + $" 快递员信息已存在，无法再次新增！";
                    }
                }
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.CooperativeCourierDatas.AddRange(data);
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
        public bool UpdateCooperativeCourierData(CooperativeCourierData data, out string Message)
        {
            Message = "";
            try
            {
                if (data== null)
                {
                    Message = "参数为空，请输入有效的合作快递员信息！";
                    return false;
                }
                if (data.StoreId <= 0)
                {
                    Message = "店铺Id为空，无法更新合作快递员信息！";
                    return false;
                }
                ///获取当前店铺所有的快递公司，用于更新前的判断
                var num = _dbContext.CooperativeCourierDatas.Where(a => a.StoreId == data.StoreId && a.CourierId==data.CourierId &&a.ID!=data.ID).Count();
                if (num > 0) {
                    Message = $" 合作快递员信息已存在，无法更新！";
                    return false;
                }
                var cour = _dbContext.CooperativeCourierDatas.Where(a => a.StoreId == data.StoreId &&  a.ID == data.ID).FirstOrDefault();
                if (cour == null) {
                    Message = $" ID {data.ID}对应的快递员信息不存在，无法更新！";
                    return false;
                }
                var userId = data.CourierId > 0 ? data.CourierId : cour.CourierId;
                var copanyId = data.ExpressCompanyId > 0 ? data.ExpressCompanyId : cour.ExpressCompanyId;
                var num1 = _dbContext.ExpressStaffDatas.Where(a => a.StoreId == data.StoreId &&a.CompanyId == copanyId && a.ID== userId).Count();
                if (num1 <= 0) {
                    Message = $"快递公司(ID：{copanyId})中不存在快递员(ID:{userId})，无法更新 ！";
                    return false;
                }
                else
                {
                    cour.ExpressCompanyId = copanyId;
                    cour.CourierId = userId;
                }
                if (data.UnitPrice > 0) {
                    cour.UnitPrice = data.UnitPrice;
                }
                if (data.CollectionNum > 0) {
                    cour.CollectionNum = data.CollectionNum;
                }
                if (data.LineOfCredit > 0)
                {
                    cour.LineOfCredit = data.LineOfCredit;
                }
                if (data.DefaultType > 0)
                {
                    cour.DefaultType = data.DefaultType;
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
        /// 获取合作快递员信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public IList<CooperativeCourierData> getCooperativeCourierDatas(CooperativeCourierData data, out string Message)
        {
            Message = "";
            List<CooperativeCourierData> list = new List<CooperativeCourierData>();
            try
            {
                if (data==null)
                {
                    Message = "参数为空，请输入有效的合作快递员信息！";
                    return list;
                }
                var storeId = data.StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，获取合作快递员信息！";
                    return list;
                }
                list = _dbContext.CooperativeCourierDatas.Where(a => a.StoreId == storeId).ToList();
                if (data.ID>0)
                {
                    list = list.Where(a => a.ID == data.ID).ToList();
                }
                if (data.ExpressCompanyId > 0)
                {
                    list = list.Where(a => a.ExpressCompanyId == data.ExpressCompanyId).ToList();
                }
                if (data.CourierId > 0)
                {
                    list = list.Where(a => a.CourierId == data.CourierId).ToList();
                }
                if (data.UnitPrice > 0)
                {
                    list = list.Where(a => a.UnitPrice == data.UnitPrice).ToList();
                }
                if (data.CollectionNum > 0)
                {
                    list = list.Where(a => a.CollectionNum == data.CollectionNum).ToList();
                }
                if (data.LineOfCredit > 0)
                {
                    list = list.Where(a => a.LineOfCredit == data.LineOfCredit).ToList();
                }
                if (data.DefaultType > 0)
                {
                    list = list.Where(a => a.DefaultType == data.DefaultType).ToList();
                }
            }
            catch (Exception e)
            {
                Message = e.Message;
                return list;
            }
            return list;
        }

        public bool DeleteCooperativeCourierDatas(IList<CooperativeCourierData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count()<=0)
                {
                    Message = "参数为空，请输入有效的合作快递员信息！";
                    return false;
                }
                foreach (var d in data) {
                    var c = _dbContext.CooperativeCourierDatas.Where(a => a.ID == d.ID && a.StoreId == d.StoreId).Count();
                    if (d.ID <= 0 || d.StoreId <= 0 || c<=0) {
                        Message = Message + "请确认合作快递员信息Id和门店Id是否输入，否则无法删除！";
                    }
                }
                if (!string.IsNullOrEmpty(Message)) {
                    return false;
                }
                _dbContext.CooperativeCourierDatas.RemoveRange(data);
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
