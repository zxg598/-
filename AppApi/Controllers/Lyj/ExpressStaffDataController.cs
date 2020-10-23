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
    public class ExpressStaffDataController : Controller
    {
        private readonly BaseDbContext _dbContext=new BaseDbContext();
        private readonly IExpressStaffDataService _service;
        public  ExpressStaffDataController(IExpressStaffDataService service) {
            _service = service;
        }

        /// <summary>
        /// 获取快递员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getExpressStaffDatas(ExpressStaffData data)
        {
            ApiResult<ExpressStaffData> result = new ApiResult<ExpressStaffData>();
            try
            {
                var list = _service.getExpressStaffDatas(data,out string Message);
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
        /// 新增快递员信息，可批量
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddExpressStaffDatas(IList<ExpressStaffData> datas)
        {
            ApiResult<ExpressStaffData> result = new ApiResult<ExpressStaffData>();
            if (datas.Count == 0)
            {
                result.IsOK = false;
                result.ErrorMessage = "参数为空!";
                return Json(result);
            }
            try
            {
                var ret = _service.CreateExpressStaffDatas(datas, out string Message);
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
        /// 更新快递员信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateExpressStaffData(ExpressStaffData data)
        {
            
            ApiResult<ExpressStaffData> result = new ApiResult<ExpressStaffData>();
            try
            {
                var ret = _service.UpdateExpressStaffData(data, out string Message);
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
        /// 删除快递员信息,可批量
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteExpressStaffDatas(IList<ExpressStaffData> datas)
        {
            ApiResult<ExpressStaffData> result = new ApiResult<ExpressStaffData>();
            try
            {
                var ret = _service.DeleteExpressStaffDatas(datas, out string Message);
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
