using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SystemFrameWork.ExtendedMethords;

namespace Portal.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AngularModule
    {
        private readonly RequestDelegate _next;

        public AngularModule(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string filePath = httpContext.Request.Path;
            if (httpContext.Request.Path.ToString().Contains("/node_modules/"))
            {
                string path = httpContext.Request.Path.ToString().Split("/node_modules/")[1].ToString();
                httpContext.Request.Path = "/node_modules/" + path;
                
            }
            if (httpContext.Request.Path.ToString().Contains("/app/"))
            {
                string path = httpContext.Request.Path.ToString().Split("/app/")[1].ToString();
                httpContext.Request.Path = "/app/" + path;

            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AngularModule>();
        }
    }
}
