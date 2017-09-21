using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApp.Models.Identity;
using WebApp.ViewModels.Account;
using SystemFrameWork.Filters.CustomAttributes;
using System.Text;
using Dal.Models.Identity;
using System.Linq;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edmonton.Controllers
{
    [WhitespaceFilter]
    public class AccountController : Controller
    {
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly SignInManager<AppIdentityUser> loginManager;
        private readonly RoleManager<AppIdentityRole> roleManager;
        private readonly BridgeToCareContext _context;

        public AccountController(UserManager<AppIdentityUser> userManager,
           SignInManager<AppIdentityUser> loginManager,
           RoleManager<AppIdentityRole> roleManager, BridgeToCareContext context)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
            _context = context;
        }
        #region Register
        // GET: /<controller>/
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel obj)
        {
            if (ModelState.IsValid)
            {
                AppIdentityUser user = new AppIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.FullName = obj.FullName;
                user.BirthDate = obj.BirthDate;

                IdentityResult result = userManager.CreateAsync
                (user, obj.Password).Result;

                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("Director").Result)
                    {
                        AppIdentityRole role = new AppIdentityRole();
                        role.Name = "Director";
                        role.Description = "Perform all operations.";
                        IdentityResult roleResult = roleManager.
                        CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("",
                             "Error while creating role!");
                            return View(obj);
                        }
                    }

                    if (!roleManager.RoleExistsAsync("Hr").Result)
                    {
                        AppIdentityRole role = new AppIdentityRole();
                        role.Name = "Hr";
                        role.Description = "Perform all Hr operations.";
                        IdentityResult roleResult = roleManager.
                        CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("",
                             "Error while creating role!");
                            return View(obj);
                        }
                    }

                    if (!roleManager.RoleExistsAsync("Employees").Result)
                    {
                        AppIdentityRole role = new AppIdentityRole();
                        role.Name = "Employees";
                        role.Description = "Perform view only.";
                        IdentityResult roleResult = roleManager.
                        CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("",
                             "Error while creating role!");
                            return View(obj);
                        }
                    }

                    userManager.AddToRoleAsync(user,
                                 obj.Role).Wait();
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(obj);
        }
        #endregion Register

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = userManager.FindByEmailAsync(obj.EmailId);
                    if (user.Result != null)
                    {
                        var result = loginManager.PasswordSignInAsync
                                        (user.Result, obj.Password,
                                          obj.RememberMe, false).Result;

                        if (result.Succeeded)
                        {
                            var userDetail = _context.AspNetUsers.ToList().Where(u => u.Email == obj.EmailId).Single();
                            var userRoll = _context.AspNetUserRoles.ToList().Where(u => u.UserId == userDetail.Id).Single();
                            var roll = _context.AspNetRoles.ToList().Where(u => u.Id == userRoll.RoleId).Single();
                            HttpContext.Session.Set("roleId", Encoding.ASCII.GetBytes(roll.Id));
                            HttpContext.Session.Set("roleName", Encoding.ASCII.GetBytes(roll.Name));
                            HttpContext.Session.Set("userId", Encoding.ASCII.GetBytes(userDetail.Id));
                            HttpContext.Session.Set("userEmail", Encoding.ASCII.GetBytes(userDetail.Email));
                            if (roll.Name.Equals("Employees"))
                            {
                                return RedirectToAction("EmployeesIndex", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }

                        }
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception.Message);
                }                           

                ModelState.AddModelError("", "Invalid login!");
            }

            return View(obj);
        }

        #endregion Login

        #region LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            try
            {
                byte[] useridB;
                var userId = HttpContext.Session.TryGetValue("userId", out useridB);
                var user = _context.AspNetUsers.Find(Encoding.ASCII.GetString(useridB));
                var userRoll = _context.AspNetUserRoles.ToList().Where(ur => ur.UserId == user.Id).ToList()[0];
                var roll = _context.AspNetRoles.ToList().Where(r => r.Id == userRoll.RoleId).Single();
                if (roll.Name == "Employees")
                {
                    try
                    {
                        byte[] emgercencyB;
                        var emgercencyEncod = HttpContext.Session.TryGetValue("EmergencyLogin", out emgercencyB);
                        var emgercency = (string.IsNullOrEmpty(Encoding.ASCII.GetString(emgercencyB))) ? false : true;
                        if (emgercency)
                        {
                            var emergencyCalls = _context.EmergencyCall.ToList().Where(a => a.UserId == user.Id).ToList();
                            foreach (var emergencyCall in emergencyCalls)
                            {
                                emergencyCall.IsActive = false;
                                emergencyCall.LogoutTime = System.DateTime.UtcNow;
                                _context.EmergencyCall.Update(emergencyCall);
                                _context.SaveChanges();
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        var assignments = _context.Assignment.ToList().Where(a => a.UserId == user.Id).ToList();
                        foreach (var assignment in assignments)
                        {
                            assignment.IsActive = false;
                            _context.Assignment.Update(assignment);
                            _context.SaveChanges();
                        }

                    }                  
                    
                    
                                     

                }
            }
            catch(Exception ex)
            {

            }
            
            loginManager.SignOutAsync().Wait();
            return RedirectToAction("Login", "Account");
        }
        #endregion LogOff

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgetPassword(ForgetPasswordViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByEmailAsync(obj.EmailId);
                if (user.Result != null)
                {
                    var token = userManager.GeneratePasswordResetTokenAsync(user.Result).Result;
                    if (!string.IsNullOrEmpty(token))
                    {
                        IdentityResult result = userManager.ResetPasswordAsync(user.Result, token, obj.Password).Result;
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    
                }
            }

            return View(); ;
        }
        #endregion
    }
}
