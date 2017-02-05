using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Portal.Filters.ActionFilters;
using Portal.Filters.ExceptionFilters;
using Portal.Filters.ResourceFilters;
using Portal.Library;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Portal
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            services.AddOptions();

            services.Configure<Appsettings>(appsettings =>
            {
                appsettings.ApplicationName = Configuration.GetSection("AppSettings:ApplicationName").Value;

            });

            services.AddMvc(config =>
            {
                config.Filters.Add(new GlobalFilter(loggerFactory));
                config.Filters.Add(new GlobalLoggingExceptionFilter(loggerFactory));
            });

            services.AddScoped<ConsoleLogActionOneFilter>();
            services.AddScoped<ConsoleLogActionTwoFilter>();
            services.AddScoped<ClassConsoleLogActionBaseFilter>();
            services.AddScoped<ClassConsoleLogActionOneFilter>();

            services.AddScoped<CustomOneLoggingExceptionFilter>();
            services.AddScoped<CustomTwoLoggingExceptionFilter>();
            services.AddScoped<CustomOneResourceFilter>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute("default", "{controller=Home}/{action=Index}");
            });

        }
    }
}

