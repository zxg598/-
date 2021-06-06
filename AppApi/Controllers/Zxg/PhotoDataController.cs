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
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SysCore.Services;
using Microsoft.AspNetCore.Http.Extensions;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PhotoDataController : Controller
    {
        private readonly BaseDbContext _dbContext=new BaseDbContext();
        private readonly IPhotoDataService _service;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _accessor;

        public PhotoDataController(IPhotoDataService service,IFileService fileService, 
            IHttpContextAccessor accessor) {
            _service = service;
            _fileService = fileService;
            _accessor = accessor;
        }


        /// <summary>
        /// 获取快递公司信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult getPhotoDatas( PhotoData data)
        {
            ApiResult<PhotoData> result = new ApiResult<PhotoData>();
            try
            {
                var list = _service.getPhotoDatas(data, out string Message);
                result.ApiData = list.ToList();
                if (!string.IsNullOrEmpty(Message))
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
        /// 新增快递公司信息，可批量
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPhotoDatas(IList<PhotoData> datas)
        {
            ApiResult<PhotoData> result = new ApiResult<PhotoData>();
            if (datas.Count == 0)
            {
                result.IsOK = false;
                result.ErrorMessage = "参数为空!";
                return Json(result);
            }
            try
            {
                var ret = _service.CreatePhotoDatas(datas, out string Message);
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
        /// 更新快递公司信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdatePhotoData(PhotoData data)
        {

            ApiResult<PhotoData> result = new ApiResult<PhotoData>();
            try
            {
                var ret = _service.UpdatePhotoDatas(data, out string Message);
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
        /// 删除快递公司信息,可批量
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeletePhotoDatas(IList<PhotoData> datas)
        {
            ApiResult<PhotoData> result = new ApiResult<PhotoData>();
            try
            {
                var ret = _service.DeletePhotoDatas(datas, out string Message);
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
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ExportExcelPhotoData() {
            ApiResult<PhotoData> result = new ApiResult<PhotoData>();
            var list = _service.getPhotoDatas(new PhotoData() { StoreId=1}, out string Message).ToList() ;
            if (Message.Length > 0)
            {
                result.IsOK = false;
                result.ErrorMessage = Message;
                return Json(result);
            }
            var dt=GlobalService.ListToDataTable<PhotoData>(list);
           
            var fileTemplatePath = @"/Template/PhotoData.xlsx";
            var outFileName = @"/Upload/PhotoData" + DateTime.Now.ToString("yyyyMMddhhmmss")  + ".xlsx";
            var filePath= _fileService.ExportExcel(fileTemplatePath, dt, outFileName);
            if (filePath.Length <= 0)
            {
                result.IsOK = false;
                result.ErrorMessage = "导出文件生成失败！";
            }
            else {
                result.FilePath = filePath;
            }

            return Json(result);
        }

    }
}
