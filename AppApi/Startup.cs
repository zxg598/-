using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using App.Data;
using Microsoft.EntityFrameworkCore;
using AppApi.Models;
using App.Services;
using Microsoft.AspNetCore.Http;
using SysCore.Models;
using Microsoft.Extensions.FileProviders;

namespace AppApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //����Swagger
            //ע��Swagger������������һ��Swagger �ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "�ӿ��ĵ�",
                    Description = "RESTful API"
                });
                // Ϊ Swagger ����xml�ĵ�ע��·��
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ////�������ݿ�
            services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            var path = AppContext.BaseDirectory + @"\App.Services.dll";
            services.RegisterService(path);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles(new StaticFileOptions
            {
                //���ó���Ĭ�ϵ�wwwroot�ļ��еľ�̬�ļ�������ļ���  �ṩ Web ��Ŀ¼����ļ�  �����������Ժ󣬾Ϳ��Է���StaticFiles�ļ��µ��ļ�
                FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles",
            });

            app.UseAuthorization();

            //�����м����������Swagger
            app.UseSwagger();
            //�����м����������SwaggerUI��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web App V1");
                c.RoutePrefix = string.Empty;//���ø��ڵ����
            });

            MyHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
