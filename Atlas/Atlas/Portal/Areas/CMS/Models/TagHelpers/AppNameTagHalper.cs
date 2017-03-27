using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using SystemFrameWork.Library;

namespace Portal.Areas.CMS.Models.TagHelpers
{
    [HtmlTargetElement("app-name")]
    [HtmlTargetElement("div", Attributes ="app-name")]
    public class AppNameTagHalper: TagHelper
    {
        private Appsettings _config;
        public AppNameTagHalper(IOptions<Appsettings> configuration)
        {
            _config = configuration.Value;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetContent("this is my tag helper from CMS " + _config.ApplicationName);
        }
    }
}
