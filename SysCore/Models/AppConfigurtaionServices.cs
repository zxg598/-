using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SysCore.Models
{
    /// <summary>
    /// 配置json处理方法
    /// </summary>
    public class AppConfigurtaionServices
    {
        ///注入IConfiguration
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaionServices()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        ///用方法直接读取
        /// <summary>
        /// 读取配置文件[AppSettings]节点数据
        /// </summary>
        public static string conn
        {
            get { return Configuration.GetConnectionString("Default"); }
        }
    }
}
