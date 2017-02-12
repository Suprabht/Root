﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Filters.CustomAttributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portal.Controllers
{
    [Route("Portal")]
    [WhitespaceFilter]
    public class AppController : Controller
    {
        [Route("index")]
        [Route("~/")]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
