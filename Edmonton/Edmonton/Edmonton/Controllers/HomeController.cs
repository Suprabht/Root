using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Edmonton.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using SystemFrameWork.Filters.CustomAttributes;
using System.Security.Claims;

namespace Edmonton.Controllers
{
    [WhitespaceFilter]
    public class HomeController : Controller
    {
        private readonly UserManager<AppIdentityUser> userManager;

        public HomeController(UserManager<AppIdentityUser>
                      userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var roles1 = claims.Where(c => c.Type == roleClaimType).ToList();
            AppIdentityUser user = userManager.GetUserAsync
                         (HttpContext.User).Result;

            ViewData["Message"] = $"Welcome {user.FullName}!";
            if (userManager.IsInRoleAsync(user, "SuperAdmin").Result)
            {
                ViewData["Message"] = ViewData["Message"] + " You are a SuperAdmin.";
            }
            

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
