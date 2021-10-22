using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DailyVisitors.DAL.Models;
using DailyVisitors.WebApi.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DailyVisitors.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly VisitorsBookContext _context;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            VisitorsBookContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExist = await userManager.FindByEmailAsync(model.Email);
            //var userExist = await userManager.FindByNameAsync(model.UserName);
            if (userExist! != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Massage = "User Already Exist" });
            var empUser = new Users();
            empUser.DisplayName = model.UserName;
            empUser.Email = model.Email;
            empUser.Active = false;
            _context.Users.Add(empUser);
            await _context.SaveChangesAsync();

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Massage = "User Ccreatio failed." });
            }
            return Ok(new Response { Status = "Success", Massage = "User Created Successfully" });
        }

        [HttpPost]
        [Route("RegisterApprover")]
        public async Task<IActionResult> RegisterApprover([FromBody] RegisterModel model)
        {
            //var userExist = await userManager.FindByNameAsync(model.UserName);
            var userExist = await userManager.FindByEmailAsync(model.Email);
            if (userExist! != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Massage = "User Already Exist" });
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Massage = "User Ccreatio failed." });
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Approver))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Approver));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Approver))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Approver);
            }
            return Ok(new Response { Status = "Success", Massage = "User Created Successfully" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var empUser = _context.Users.FirstOrDefault(user => user.Email == model.Email);
            if( Convert.ToBoolean(empUser.Active))
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user! != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    var authClames = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                    foreach (var userRole in userRoles)
                    {
                        authClames.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSighKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudiance"],
                        expires: DateTime.Now.AddHours(2),
                        claims: authClames,
                        signingCredentials: new SigningCredentials(authSighKey, SecurityAlgorithms.HmacSha256)
                        );

                    //var Users = _context.Users.FirstOrDefault(user => user.Email == model.Email);

                    var Users = new Users
                    {
                        UserId = 1,
                        FirstName = "Suprabhat",
                        Email = "suprabhatpaul@sdl.com",
                        LastName = "Paul",
                        DisplayName = "suprabhatpaul"
                    };

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        userEmail = Users.Email,
                        userName = Users.DisplayName
                    });
                }
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel usermodel)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await userManager.FindByIdAsync(userId);
            var user = await userManager.FindByEmailAsync(usermodel.Email);
            var result = await userManager.ChangePasswordAsync(user, usermodel.OldPassword, usermodel.NewPassword);
            if (!result.Succeeded)
            {
                return Ok(new
                {
                    result = "Error",
                    text = result.Errors.First().Description.ToString()
                });
            }
            return Ok(new
            {
                result = "Succeeded",
                text = "Your password has been changed successfully"
            });
        }
    }
}
