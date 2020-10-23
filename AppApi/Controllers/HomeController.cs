using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using AppApi.Models;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly BaseDbContext _dbContext=new BaseDbContext();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult getTest()
        {
            ApiResult<Test> result = new ApiResult<Test>();
            try
            {
                var list = _dbContext.Tests.ToList();
                result.ApiData = list;
            }
            catch (Exception ex) {
                result.IsOK = false;
                result.ErrorMessage = ex.Message;
            }
            return Json(result);
        }

       

       /// <summary>
       /// 根据名称获取列表
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
        [HttpGet]
        public IActionResult getTestByName(string name)
        {           
            ApiResult<Test> result = new ApiResult<Test>();
            try
            {
                var list = _dbContext.Tests.Where(a=>a.Name==name).ToList();
                result.ApiData = list;
            }
            catch (Exception ex)
            {
                result.IsOK = false;
                result.ErrorMessage = ex.Message;
            }
            return Json(result);
        }

       /// <summary>
       /// 新增Test数据，可批量
       /// </summary>
       /// <param name="tests"></param>
       /// <returns></returns>
        [HttpPost]
        public IActionResult AddTest(IList<Test> tests)
        {
            ApiResult<Test> result = new ApiResult<Test>();
            if (tests.Count == 0)
            {
                result.IsOK = false;
                result.ErrorMessage = "参数为空!";
                return Json(result);
            }
            try
            {
                _dbContext.Tests.AddRange(tests);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.IsOK = false;
                result.ErrorMessage = ex.Message;
            }
            return Json(result);
        }

        /// <summary>
        /// 更新Test数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateTest(Test test)
        {
            
            ApiResult<Test> result = new ApiResult<Test>();
            try
            {
                var t = _dbContext.Tests.Where(a => a.Id == test.Id).FirstOrDefault();
                t.Name = test.Name;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.IsOK = false;
                result.ErrorMessage = ex.Message;
            }
            return Json(result);
        }

       /// <summary>
       /// 删除Test信息,可批量
       /// </summary>
       /// <param name="tests"></param>
       /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteTest(IList<Test> tests)
        {
            ApiResult<Test> result = new ApiResult<Test>();
            try
            {
                _dbContext.RemoveRange(tests);
                _dbContext.SaveChanges();
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
