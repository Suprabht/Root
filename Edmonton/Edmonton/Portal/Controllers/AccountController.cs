using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemFrameWork.Filters.CustomAttributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        [WhitespaceFilter]
        // GET: /<controller>/
        [Route("Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult Activate()
        {
            return View();
        }
    }
}
