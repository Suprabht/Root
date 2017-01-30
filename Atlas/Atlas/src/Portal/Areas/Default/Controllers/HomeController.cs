using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Portal.Areas.Default.Models;

namespace Portal.Areas.Default.Controllers
{
    [Area("Default")]
    [Route("default/home")]
    [Route("home")]
    public class HomeController : Controller
    {
        private Appsettings _configuration;
        public HomeController(IOptions<Appsettings> configuration)
        {
            _configuration = configuration.Value;
        }

        // GET: /<controller>/
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            ViewBag.AppName = _configuration.ApplicationName;
            ViewBag.Title = "foo bag";
            return View("Index", new Person { Name = "Raj" });
        }

    }
}
