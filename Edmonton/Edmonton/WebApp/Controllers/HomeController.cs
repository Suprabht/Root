using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SystemFrameWork.Filters.CustomAttributes;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebApp.Models.Identity;
using SystemFrameWork.WebHelper;
using Microsoft.Extensions.Options;
using Dal.Models.Identity;

namespace WebApp.Controllers
{
    [WhitespaceFilter]
    public class HomeController : Controller
    {
        private Appsettings _configuration;
        private BridgeToCareContext _context;
        public HomeController(IOptions<Appsettings> configuration, BridgeToCareContext context)
        {
            _configuration = configuration.Value;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Roles()
        {
            var jsonString = "[";
            foreach (var role in _context.AspNetRoles.ToList())
            {
                jsonString += "{";
                jsonString += "\"Role\":\"" + role.Name + "\",";
                jsonString += "\"Id\":\"" + role.Id + "\",";
                jsonString += "\"Description\":\"" + role.Description + "\",";
                jsonString += "\"Users\":[";
                foreach (var userRoles in _context.AspNetUserRoles.ToList().Where(n => n.RoleId==role.Id).ToList())
                {
                    
                    if (userRoles.UserId != null)
                    {
                        var user = _context.AspNetUsers.ToList().Where(u => u.Id == userRoles.UserId);
                        if(user != null)
                        {
                            jsonString += "{";
                            jsonString += "\"Email\":\"" + userRoles.User.Email + "\",";
                            jsonString += "\"Id\":\"" + userRoles.User.Id + "\"";
                            jsonString += "},";
                        }                        
                    }                    
                }
                jsonString = jsonString.TrimEnd(',');
                jsonString += "]";
                jsonString += "},";
            }
            jsonString = jsonString.TrimEnd(',');
            jsonString += "]";
            ViewData["Message"] = "Your Roles page.";

            return View();
        }

        public IActionResult ProgramDetails()
        {
            ViewData["Message"] = "Your Program Details page.";

            return View();
        }
        public IActionResult UserRegistration()
        {
            ViewData["Message"] = "Your User Registration page.";

            return View();
        }
        public IActionResult PaymentDetails()
        {
            ViewData["Message"] = "Your Payment Details page.";

            return View();
        }
        public IActionResult UserInfo()
        {
            ViewData["Message"] = "Your User Info page.";

            return View();
        }
        public IActionResult UserLevelDetails()
        {
            ViewData["Message"] = "Your User Level Details page.";

            return View();
        }
        public IActionResult DataBackup()
        {
            ViewData["Message"] = "Your Data Backup page.";

            return View();
        }

        public IActionResult TaskAssignment()
        {
            ViewData["Message"] = "Your Task Assignment page.";

            return View();
        }
        public IActionResult AssignmentPlan()
        {
            ViewData["Message"] = "Your Assignment Plan page.";

            return View();
        }
        public IActionResult PaymentView()
        {
            ViewData["Message"] = "Your Payment View page.";

            return View();
        }
        public IActionResult LeavePlanner()
        {
            ViewData["Message"] = "Your Leave Planner page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
