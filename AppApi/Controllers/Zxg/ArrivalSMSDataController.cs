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
    public class ArrivalSMSDataController : Controller
    {
        private readonly BaseDbContext _dbContext=new BaseDbContext();
        private readonly IArrivalSMSDataService _service;
        public ArrivalSMSDataController(IArrivalSMSDataService service) {
            _service = service;
        }

        /// <summary>
        /// 获取合作快递员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getArrivalSMSDatas(ArrivalSMSData data)
        {
            ApiResult<ArrivalSMSData> result = new ApiResult<ArrivalSMSData>();
            try
            {
                var list = _service.getArrivalSMSDatas(data,out string Message);
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
        public IActionResult AddArrivalSMSDatas(IList<ArrivalSMSData> datas)
        {
            ApiResult<ArrivalSMSData> result = new ApiResult<ArrivalSMSData>();
            if (datas.Count == 0)
            {
                result.IsOK = false;
                result.ErrorMessage = "参数为空!";
                return Json(result);
            }
            try
            {
                var ret = _service.CreateArrivalSMSDatas(datas, out string Message);
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
        public IActionResult UpdateArrivalSMSData(ArrivalSMSData data)
        {
            
            ApiResult<ArrivalSMSData> result = new ApiResult<ArrivalSMSData>();
            try
            {
                var ret = _service.UpdateArrivalSMSDatas(data, out string Message);
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
        public IActionResult DeleteArrivalSMSDatas(IList<ArrivalSMSData> datas)
        {
            ApiResult<ArrivalSMSData> result = new ApiResult<ArrivalSMSData>();
            try
            {
                var ret = _service.DeleteArrivalSMSDatas(datas, out string Message);
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
