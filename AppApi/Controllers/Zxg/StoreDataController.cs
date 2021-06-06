using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using AppApi.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StoreDataController : Controller
    {
        private readonly BaseDbContext _dbContext=new BaseDbContext();
        private readonly IStoreDataService _service;
        public StoreDataController(IStoreDataService service) {
            _service = service;
        }

        /// <summary>
        /// 获取合作快递员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getStoreDatas(StoreData data)
        {
            ApiResult<StoreData> result = new ApiResult<StoreData>();
            try
            {
                var list = _service.getStoreDatas(data,out string Message);
                result.ApiData = list.ToList();
                if (!string.IsNullOrEmpty(Message)) {
                    result.IsOK = false;
                    result.ErrorMessage = Message;
                }
            }
            catch (Exception ex) {
                result.IsOK = false;
                result.ErrorMessage = ex.Message;
            }
            return Json(result);
        }


        /// <summary>
        /// 新增合作快递员信息，可批量
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddStoreDatas(IList<StoreData> datas)
        {
            ApiResult<StoreData> result = new ApiResult<StoreData>();
            if (datas.Count == 0)
            {
                result.IsOK = false;
                result.ErrorMessage = "参数为空!";
                return Json(result);
            }
            try
            {
                var ret = _service.CreateStoreDatas(datas, out string Message);
                if (ret == false)
                {
                    result.IsOK = false;
                    result.ErrorMessage = Message;
                }
            }
            catch (Exception ex)
            {
                result.IsOK = false;
                result.ErrorMessage = ex.Message;
            }
            return Json(result);
        }

        /// <summary>
        /// 更新合作快递员信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateStoreData(StoreData data)
        {
            
            ApiResult<StoreData> result = new ApiResult<StoreData>();
            try
            {
                var ret = _service.UpdateStoreData(data, out string Message);
                if (ret == false) {
                    result.IsOK = false;
                    result.ErrorMessage = Message;
                }
            }
            catch (Exception ex)
            {
                result.IsOK = false;
                result.ErrorMessage = ex.Message;
            }
            return Json(result);
        }

        /// <summary>
        /// 删除合作快递员信息,可批量
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteStoreDatas(IList<StoreData> datas)
        {
            ApiResult<StoreData> result = new ApiResult<StoreData>();
            try
            {
                var ret = _service.DeleteStoreDatas(datas, out string Message);
                if (ret == false)
                {
                    result.IsOK = false;
                    result.ErrorMessage = Message;
                }
            }
            catch (Exception ex)
            {
                result.IsOK = false;
                result.ErrorMessage = ex.Message;
            }
            return Json(result);
        }


    }
}
