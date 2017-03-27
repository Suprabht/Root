using Microsoft.AspNetCore.Mvc.Filters;
using SystemFrameWork.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SystemFrameWork.Filters.CustomAttributes
{
    public class WhitespaceFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            response.Body = new WhiteSpaceFilter(response.Body, s =>
            {
                s = Regex.Replace(s, @"\s+", " ");
                s = Regex.Replace(s, @"\s*\n\s*", "\n");
                s = Regex.Replace(s, @"\s*\>\s*\<\s*", "><");
                s = Regex.Replace(s, @"<!--(.*?)-->", "");   //Remove comments

                // single-line doctype must be preserved 
                var firstEndBracketPosition = s.IndexOf(">");
                if (firstEndBracketPosition >= 0)
                {
                    s = s.Remove(firstEndBracketPosition, 1);
                    s = s.Insert(firstEndBracketPosition, ">");
                }
                return s;
            });

        }

    }
}
