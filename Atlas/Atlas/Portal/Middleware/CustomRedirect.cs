using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using SystemFrameWork.ExtendedMethords;

namespace Portal.Middleware
{
    public class CustomRedirect : Microsoft.AspNetCore.Rewrite.IRule
    {

        public void ApplyRule(RewriteContext context)
        {
            /*
            var request = context.HttpContext.Request;
            var host = request.Host;

            // Exclude localhost
            if (string.Equals(host.Host, "localhost", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            // Exclude api
            if (request.Path.Value.Contains("/api/"))
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            // force other traffic to https
            if (string.Equals(request.Scheme, "http", StringComparison.OrdinalIgnoreCase))
            {
                string path = "https://" + host.Value + request.PathBase + request.Path + request.QueryString;
                context.HttpContext.Response.Redirect(path, true);
                context.Result = RuleResult.EndResponse;
            }
            */
           // if (context.HttpContext.Request.Path.ToString().Contains("/node_modules/"))
           // {
                //string path = context.HttpContext.Request.Path.ToString().Split("/node_modules/")[1].ToString();
                //context.HttpContext.Response.Redirect("/node_modules/" + path, true);
                //context.Result = RuleResult.EndResponse;
           // }
        }
    }
}
