using App.Data;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services
{
    public class PhotoDataService : IPhotoDataService
    {
        BaseDbContext _dbContext = new BaseDbContext();

        /// <summary>
        /// 新增照片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool CreatePhotoDatas(IList<PhotoData> data, out string Message)
        {
            Message = "";
            try
            {
                if (data.Count() <= 0)
                {
                    Message = "参数为空，请输入有效的照片信息！";
                    return false;
                }
                var storeId = data[0].StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，无法新增照片！";
                    return false;
                } 
                //判断是否为空，若为空，则批量新增，否则返回false
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.PhotoDatas.AddRange(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新照片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool UpdatePhotoDatas(PhotoData data, out string Message)
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
        /// 获取照片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public IList<PhotoData> getPhotoDatas(PhotoData data, out string Message)
        {
            Message = "";
            List<PhotoData> list = new List<PhotoData>();
            try
            {
                if (data==null)
                {
                    Message = "参数为空，请输入有效的照片！";
                    return list;
                }
                var storeId = data.StoreId;
                if (storeId <= 0)
                {
                    Message = "店铺Id为空，获取照片信息！";
                    return list;
                }
                list = _dbContext.PhotoDatas.Where(a => a.StoreId == storeId).ToList();
               
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
        /// <summary>
        /// 删除照片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public bool DeletePhotoDatas(IList<PhotoData> data, out string Message)
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
                    var num = _dbContext.PhotoDatas.Where(a => a.ID == d.ID && a.StoreId == d.StoreId).Count();
                    if (d.ID <= 0 || d.StoreId <= 0 || num <=0)
                    {
                        Message = Message + "请确认照片Id和门店Id是否输入，否则无法删除！";
                    } 
                }
                if (!string.IsNullOrEmpty(Message))
                {
                    return false;
                }
                _dbContext.PhotoDatas.RemoveRange(data);
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
