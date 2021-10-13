using DailyVisitors.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using DailyVisitors.EnumConstants;
using DinkToPdf.Contracts;
using DinkToPdf;
using DailyVisitors.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DailyVisitors.WebApi.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace DailyVisitors.WebApi
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});

			services.Configure<FormOptions>(o =>
			{
				o.ValueLengthLimit = int.MaxValue;
				o.MultipartBodyLengthLimit = int.MaxValue;
				o.MemoryBufferThreshold = int.MaxValue;
			});

			services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
			services.AddScoped<IReportService, ReportService>();
			services.AddControllers();
			Settings.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
			Settings.SmtpHost = Configuration.GetValue<string>("Smtp:SmtpHost");
			Settings.SmtpPort = Configuration.GetValue<int>("Smtp:SmtpPort");
			Settings.SmtpPass = Configuration.GetValue<string>("Smtp:SmtpPass");
			Settings.SmtpUser = Configuration.GetValue<string>("Smtp:SmtpUser");
			Settings.From = Configuration.GetValue<string>("Smtp:From");

			services.AddDbContext<VisitorsBookContext>(options => options.UseSqlServer(Settings.ConnectionString));
			// Entity Framework
			services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			// For identity
			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();
			// Adding Authintication
			services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			// Adding Jwt Bearer
			.AddJwtBearer(option =>
			{
				option.SaveToken = true;
				option.RequireHttpsMetadata = false;
				option.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = Configuration["JWT:ValidAudiance"],
					ValidIssuer = Configuration["JWT:ValidIssuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))

				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
				RequestPath = new PathString("/StaticFiles")
			});
			app.UseRouting();
			app.UseCors("CorsPolicy");
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
