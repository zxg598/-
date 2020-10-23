using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppApi.Models
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 获取程序集中实现类对应的多个接口
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Dictionary<Type, Type[]> GetClassName(string assemblyName) {
            if (!string.IsNullOrEmpty(assemblyName)) {
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in ts.Where(s => !s.IsInterface)) {
                    var interfaceType = item.GetInterfaces();
                    if (interfaceType.Count() > 0) {
                        result.Add(item, interfaceType);
                    }
                    
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }

        /// <summary>
        /// 注册多个程序集服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyNames"></param>
        public static void RegisterService(this IServiceCollection services, List<string> assemblyNames) {
            foreach (var assemlyName in assemblyNames) {
                services.RegisterService(assemlyName);
            }
        }

        /// <summary>
        /// 注册单个程序集服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        public static void RegisterService(this IServiceCollection services, string assemblyName) {
            foreach (var item in GetClassName(assemblyName)){
                foreach (var typeArray in item.Value) {
                    services.AddScoped(typeArray, item.Key);
                }
            }
        }
    
    }
}
