using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewComponents
{
    public class NavBarViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var menu = new Menu();
            if (roles.Count>0)
            {
                if (roles[0].Value.Equals("Director"))
                {
                    menu.IsRoles = true;
                    menu.IsUserRegistration = true;
                    menu.IsUserInfo = true;
                    menu.IsTaskAssignment = true;
                    menu.IsProgramDetails = true;
                    menu.IsPaymentDetails = true;
                    menu.IsDataBackup = true;

                }

                if (roles[0].Value.Equals("Hr"))
                {
                    menu.IsRoles = true;
                    menu.IsUserRegistration = true;
                    menu.IsUserInfo = true;
                    menu.IsTaskAssignment = true;
                    menu.IsUserLevelDetail = true;
                }

                if (roles[0].Value.Equals("Employees"))
                {
                    menu.IsAssignmentPlan = true;
                    menu.IsPaymentView = true;
                    menu.IsLeavePlanner = true;
                }
            }
            
            return View(menu);
        }
    }
}
