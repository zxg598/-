using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using AppApi.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RetentionTimeDataController : Controller
    {
        private readonly BaseDbContext _dbContext=new BaseDbContext();
        private readonly IRetentionTimeDataService _service;
        
        public  RetentionTimeDataController(IRetentionTimeDataService service) {
            _service = service;
        }

        /// <summary>
        /// 获取滞留时间配置信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getRetentionTimeDatas(RetentionTimeData data)
        {
            ApiResult<RetentionTimeData> result = new ApiResult<RetentionTimeData>();
            try
            {
                var list = _service.getRetentionTimeDatas(data,out string Message);
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
        /// 新增滞留时间配置信息，可批量
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRetentionTimeDatas(IList<RetentionTimeData> datas)
        {
            ApiResult<RetentionTimeData> result = new ApiResult<RetentionTimeData>();
            if (datas.Count == 0)
            {
                result.IsOK = false;
                result.ErrorMessage = "参数为空!";
                return Json(result);
            }
            try
            {
                var ret = _service.CreateRetentionTimeDatas(datas, out string Message);
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
        /// 更新滞留时间配置信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateRetentionTimeData(RetentionTimeData data)
        {
            
            ApiResult<RetentionTimeData> result = new ApiResult<RetentionTimeData>();
            try
            {
                var ret = _service.UpdateRetentionTimeData(data, out string Message);
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
        /// 删除滞留时间配置信息,可批量
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteRetentionTimeDatas(IList<RetentionTimeData> datas)
        {
            ApiResult<RetentionTimeData> result = new ApiResult<RetentionTimeData>();
            try
            {
                var ret = _service.DeleteRetentionTimeDatas(datas, out string Message);
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
