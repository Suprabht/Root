
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;



namespace Portal
{
    //public class AppSettings
    //{
    //    public string ApplicationName { get; set; }
    //    public int Version { get; set; }
    //}
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
               // builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                //builder.AddApplicationInsightsSettings(developerMode: true);
            }
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
                
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();
            // Add functionality to inject IOptions<T>
          //  services.AddOptions();

            // Add our Config object so it can be injected
            //services.Configure<AppSettings>(appSettings => {
            //    appSettings.ApplicationName = Configuration.GetSection("");
            //});
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            
            app.UseMvc(routes=> {
                routes.MapRoute("default", "{controller=Home}/{action=Index}");
            });
            
            //app.Run(async (context) =>
            //{
            //    string greeting = configuration.GetSection("greeting").Value;
            //    await context.Response.WriteAsync(greeting);
            //});
        }
    }
}
