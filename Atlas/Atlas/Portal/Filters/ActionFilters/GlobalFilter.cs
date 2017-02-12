using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Portal.Library;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Portal.Filters.ActionFilters
{
    public class GlobalFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public GlobalFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("GlobalFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            string path = context.HttpContext.Request.Path.Value.Trim().ToLower();
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("OnActionExecuted");
            base.OnActionExecuted(context);           
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation("OnResultExecuting");           
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("OnResultExecuted");
            base.OnResultExecuted(context); 
            
        }
    }
}
