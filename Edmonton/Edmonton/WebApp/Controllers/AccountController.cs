using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApp.Models.Identity;
using WebApp.Models.ViewModels;
using SystemFrameWork.Filters.CustomAttributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edmonton.Controllers
{
    [WhitespaceFilter]
    public class AccountController : Controller
    {
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly SignInManager<AppIdentityUser> loginManager;
        private readonly RoleManager<AppIdentityRole> roleManager;


        public AccountController(UserManager<AppIdentityUser> userManager,
           SignInManager<AppIdentityUser> loginManager,
           RoleManager<AppIdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
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
                var user = userManager.FindByEmailAsync(obj.EmailId);
                if (user.Result != null)
                {
                    var result = loginManager.PasswordSignInAsync
                                    (user.Result, obj.Password,
                                      obj.RememberMe, false).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
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
