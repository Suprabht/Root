using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Portal.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        
        // GET: /<controller>/
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {

            return View();
        }
    }
}
