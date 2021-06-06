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
    public class SwitchSettingsDataController : Controller
    {
        private readonly BaseDbContext _dbContext=new BaseDbContext();
        private readonly ISwitchSettingsDataService _service;
        public  SwitchSettingsDataController(ISwitchSettingsDataService service) {
            _service = service;
        }

        /// <summary>
        /// 获取快递员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getSwitchSettingsDatas(SwitchSettingsData data)
        {
            ApiResult<SwitchSettingsData> result = new ApiResult<SwitchSettingsData>();
            try
            {
                var list = _service.getSwitchSettingsDatas(data,out string Message);
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
        public IActionResult AddSwitchSettingsDatas(IList<SwitchSettingsData> datas)
        {
            ApiResult<SwitchSettingsData> result = new ApiResult<SwitchSettingsData>();
            if (datas.Count == 0)
            {
                result.IsOK = false;
                result.ErrorMessage = "参数为空!";
                return Json(result);
            }
            try
            {
                var ret = _service.CreateSwitchSettingsDatas(datas, out string Message);
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
        public IActionResult UpdateSwitchSettingsData(SwitchSettingsData data)
        {
            
            ApiResult<SwitchSettingsData> result = new ApiResult<SwitchSettingsData>();
            try
            {
                var ret = _service.UpdateSwitchSettingsData(data, out string Message);
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
        public IActionResult DeleteSwitchSettingsDatas(IList<SwitchSettingsData> datas)
        {
            ApiResult<SwitchSettingsData> result = new ApiResult<SwitchSettingsData>();
            try
            {
                var ret = _service.DeleteSwitchSettingsDatas(datas, out string Message);
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
