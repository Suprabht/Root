using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Portal.Areas.CMS.Models;
using SystemFrameWork.WebHelper;
using SystemFrameWork.Filters.CustomAttributes;

namespace Portal.Areas.CMS.Controllers
{
    [Area("CMS")]
    [Route("CMS/home")]
    [WhitespaceFilter]
    // [Route("home")] Note possible
    public class HomeController : Controller
    {
        private Appsettings _configuration;
        public HomeController(IOptions<Appsettings> configuration)
        {
            _configuration = configuration.Value;
        }

        // GET: /<controller>/
        [Route("index")]
        
        public IActionResult Index()
        {
            ViewBag.AppName = _configuration.ApplicationName;
            ViewBag.Title = "CMS....";
            return View("Index", new Person { Name = "Raj Aryan"});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("home/submit", Name ="Submit")]
        public IActionResult Submit()
        {           
            return View("Index", new Person { Name = "Raj" });
        }
    }
}
