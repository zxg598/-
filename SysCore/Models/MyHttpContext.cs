
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysCore.Models
{
    public  class MyHttpContext
    {
        private static IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// 当前上下文
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current => _contextAccessor.HttpContext;


        //HttpContext.Request.GetDisplayUrl()

        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

    }
}
