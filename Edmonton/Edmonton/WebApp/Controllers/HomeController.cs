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
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Home;
using SystemFramework.Json;

namespace WebApp.Controllers
{
    [WhitespaceFilter]
    public class HomeController : Controller
    {
        private Appsettings _configuration;
        private readonly BridgeToCareContext _context;
        private readonly UserManager<AppIdentityUser> _userManager;
        public HomeController(UserManager<AppIdentityUser> userManager, IOptions<Appsettings> configuration, BridgeToCareContext context)
        {
            _configuration = configuration.Value;
            _context = context;
            _userManager = userManager;
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

        #region User
       // [HttpGet]
        public IActionResult GetUsers()
        {
            var users = new List<UserDetails>();
            var role = _context.AspNetRoles
                    .Where(b => b.Name == "Employees")
                    .FirstOrDefault();

            foreach (var userRoles in _context.AspNetUserRoles.ToList().Where(n => n.RoleId == role.Id).ToList())
            {
                if (userRoles.UserId != null)
                {
                    var user = _context.AspNetUsers.ToList().Where(u => u.Id == userRoles.UserId).Single();
                    users.Add(new UserDetails {
                        Id = user.Id,
                        Name = user.FullName,
                        Email = user.Email,
                        Address = user.Address,
                        AlternateEmail = user.AlternetEmail,
                        AlternetPhone = user.AlternetPhone,
                        BloodGroup = user.BloodGroup,
                        Phone = user.PhoneNumber
                    });
                }
            }
            return Json(new { page = 1, records = users.Count, rows = users });

        }
        #endregion

        #region Roles
        [Authorize]
        public IActionResult Roles()
        {
            ViewData["Message"] = "Your Roles page.";

            return View();
        }

        public IActionResult RolesTree()
        {
            return ViewComponent("RolesTree");
        }
        public IActionResult UserDetails(string id)
        {
            //var user = _context.AspNetUsers.ToList().Where(n => n.Id == id).ToList()[0];
            var user = _context.AspNetUsers.Find(id);

            UserDetails userDetails = new UserDetails
            {
                Id = user.Id,
                Name = user.FullName,
                Email = user.Email,
                AlternateEmail = user.AlternetEmail,
                Address = user.Address,
                Phone = user.PhoneNumber,
                AlternetPhone = user.AlternetPhone,
                BloodGroup = user.BloodGroup

            };

            return ViewComponent("UserDetails", userDetails);
        }

        public IActionResult AddUser(string id)
        {
            var roll = _context.AspNetRoles.Find(id);
            var userDetails = new UserDetails
            {
                AddView = true,
                RoleId = id,
                RoleName = roll.Name
            };
            return ViewComponent("UserDetails", userDetails);
        }

        [HttpPost]
        public IActionResult AddUserDetails(UserDetails userDetails)
        {
            if (userDetails == null)
            {
                return NotFound();
            }
            AppIdentityUser user = new AppIdentityUser();
            user.UserName = userDetails.Name;
            user.PhoneNumber = userDetails.Phone;
            user.Email = userDetails.Email;
            user.FullName = userDetails.Name;
            user.AlternetEmail = userDetails.AlternateEmail;
            user.Address = userDetails.Address;
            user.AlternetPhone = userDetails.AlternetPhone;
            user.BloodGroup = userDetails.BloodGroup;
            IdentityResult result = _userManager.CreateAsync
                (user, "Password123!").Result;
            if (result.Succeeded)
            {
                var roll = _context.AspNetRoles.Find(userDetails.RoleId);
                _userManager.AddToRoleAsync(user,
                    roll.Name).Wait();
            }
            return Json(new { Response = "Success", userId = user.Id, roleId = userDetails.RoleId });
        }

        [HttpPost]
        public IActionResult UpdateUserDetails(UserDetails userDetails)
        {
            if (userDetails == null)
            {
                return NotFound();
            }
            var userToUpdate = _context.AspNetUsers.Find(userDetails.Id);
            userToUpdate.PhoneNumber = userDetails.Phone;
            userToUpdate.Email = userDetails.Email;
            userToUpdate.FullName = userDetails.Name;
            userToUpdate.AlternetEmail = userDetails.AlternateEmail;
            userToUpdate.Address = userDetails.Address;
            userToUpdate.AlternetPhone = userDetails.AlternetPhone;
            userToUpdate.BloodGroup = userDetails.BloodGroup;
            _context.AspNetUsers.Update(userToUpdate);
            _context.SaveChanges();

            /*  var userToUpdate = await _context.AspNetUsers.SingleOrDefaultAsync(s => s.Id == userDetails.Id);
              if (await TryUpdateModelAsync<AspNetUsers>(
                  userToUpdate,
                  "",
                  s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
              {
                  try
                  {
                      await _context.SaveChangesAsync();
                      return RedirectToAction("Index");
                  }
                  catch (DbUpdateException )
                  {
                      //Log the error (uncomment ex variable name and write a log.)
                      ModelState.AddModelError("", "Unable to save changes. " +
                                                   "Try again, and if the problem persists, " +
                                                   "see your system administrator.");
                  }
              }
              return View(studentToUpdate);*/

            return Json(new { Response = "Success" });
        }
        #endregion

        #region ProgramDetails
        [Authorize]
        public IActionResult ProgramDetails()
        {
            ViewData["Message"] = "Your Program Details page.";

            return View();
        }

        [HttpGet]
        public IActionResult Program()
        {
            var programs = _context.ProgramDetails.ToList();
            var jsonstring = JsonHelper.Serialize(programs);
            return Json(new { page = 1, records = programs.Count, rows = programs });
        }

        [HttpPost]
        public IActionResult Program(int id, ProgramDetails programDetails)
        {
            if (programDetails == null)
            {
                return NotFound();
            }
            var programDetailsToUpdate = _context.ProgramDetails.Find(programDetails.ProgramId);
            programDetailsToUpdate.ProgramName = programDetails.ProgramName;
            programDetailsToUpdate.ProgramDescription = programDetails.ProgramDescription;
            _context.ProgramDetails.Update(programDetailsToUpdate);
            _context.SaveChanges();
            return Json(new { Response = "Success" });
        }

        [HttpPut]
        public IActionResult Program(ProgramDetails programDetails)
        {
            _context.ProgramDetails.Add(programDetails);
            _context.SaveChanges();
            return Json(new { Response = "Success" });
        }
        [HttpDelete]
        public IActionResult Program(int id)
        {
            var programDetails = _context.ProgramDetails.Find(id);
            if (programDetails != null)
            {
                _context.Entry(programDetails).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return Json(new { Response = "Success" });
        }
        #endregion

        #region Clients
        [Authorize]
        public IActionResult Clients()
        {
            ViewData["Message"] = "Your Client page.";

            return View();
        }
        [HttpGet]
        public IActionResult Client()
        {
            var clientsDal = _context.ClientDetails.ToList();
            List<Client> clients = new List<Client>();
            foreach (var clientDal in clientsDal)
            {
                var client = new Client();
                client.ClientAddress = clientDal.ClientAddress;
                client.ClientId = clientDal.ClientId;
                client.ClientName = clientDal.ClientName;
                client.Latt = clientDal.Latt;
                client.Long = clientDal.Long;
                client.Link = string.Format("http://maps.google.com/maps?q={0},{1}", clientDal.Latt, clientDal.Long);
                clients.Add(client);
            }
            return Json(new { page = 1, records = clients.Count, rows = clients });
        }

        [HttpPost]
        public IActionResult Client(int id, Client client)
        {
            var clientToUpdate = _context.ClientDetails.Find(client.ClientId);

            clientToUpdate.ClientAddress = client.ClientAddress;
            clientToUpdate.ClientName = client.ClientName;
            clientToUpdate.Latt = client.Latt;
            clientToUpdate.Long = client.Long;
            _context.ClientDetails.Update(clientToUpdate);
            _context.SaveChanges();
            return Json(new { Response = "Success" });
        }

        [HttpPut]
        public IActionResult Client(Client client)
        {
            var clientDetails = new ClientDetails()
            {
                ClientId = client.ClientId,
                ClientAddress = client.ClientAddress,
                ClientName = client.ClientName,
                Latt = client.Latt,
                Long = client.Long
            };
            _context.ClientDetails.Add(clientDetails);
            _context.SaveChanges();
            return Json(new { Response = "Success" });
        }

        [HttpDelete]
        public IActionResult Client(int id)
        {
            var clientDetails = _context.ClientDetails.Find(id);
            if (clientDetails != null)
            {
                _context.Entry(clientDetails).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return Json(new { Response = "Success" });
        }
        #endregion

        #region TaskAssignment
        public IActionResult TaskAssignmentDetails()
        {
            ViewData["Message"] = "Your Task Assignment page.";

            return View();
        }

        [HttpGet]
        public IActionResult TaskAssignment()
        {
            List<Models.Home.Assignment> assigns = new List<Models.Home.Assignment>();
            var assignmentsDal = _context.Assignment.ToList();
            foreach (var assignmentDal in assignmentsDal)
            {
                Models.Home.Assignment assign = new Models.Home.Assignment();
                assign.AssignmentId = assignmentDal.AssignmentId;
                assign.AssignmentDate = Convert.ToDateTime(assignmentDal.AssignmentDate);
                assign.ClientId = Convert.ToInt16(assignmentDal.ClientId);
                var client = _context.ClientDetails.Find(assign.ClientId);
                assign.ClientName = client.ClientName;
                assign.ClientAddress = client.ClientAddress;
                assign.UserId = assignmentDal.UserId;
                var user = _context.AspNetUsers.Find(assign.UserId);
                assign.UserName = user.UserName;
                assign.UserEmail = user.Email;
                assigns.Add(assign);
            }
            var jsonstring = JsonHelper.Serialize(assigns);
            return Json(new { page = 1, records = assignmentsDal.Count, rows = assigns });
        }

        [HttpPost]
        public IActionResult TaskAssignment(int id, ProgramDetails programDetails)
        {
            return Json(new { Response = "Success" });
        }

        [HttpPut]
        public IActionResult TaskAssignment(ProgramDetails programDetails)
        {
            
            return Json(new { Response = "Success" });
        }

        [HttpDelete]
        public IActionResult TaskAssignment(int id)
        {
            return Json(new { Response = "Success" });
        }
        #endregion

        #region UserLevel
        [Authorize]
        public IActionResult UserLevelDetails()
        {
            ViewData["Message"] = "Your User Level Details page.";

            return View();
        }

        [HttpGet]
        public IActionResult UserLevel()
        {
            var programs = _context.UserLevel.ToList();
            var jsonstring = JsonHelper.Serialize(programs);
            return Json(new { page = 1, records = programs.Count, rows = programs });
        }

        [HttpPost]
        public IActionResult UserLevel(int id, UserLevel userLevel)
        {
            if (userLevel == null)
            {
                return NotFound();
            }
            var userLevelToUpdate = _context.UserLevel.Find(userLevel.UserLevelId);
            userLevelToUpdate.UserLevelName = userLevel.UserLevelName;
            userLevelToUpdate.UserLevelDescription = userLevel.UserLevelDescription;
            _context.UserLevel.Update(userLevelToUpdate);
            _context.SaveChanges();
            return Json(new { Response = "Success" });
        }

        [HttpPut]
        public IActionResult UserLevel(UserLevel userLevel)
        {
            //if (ModelState.IsValid)
            //{
            _context.UserLevel.Add(userLevel);
            _context.SaveChanges();
            return Json(new { Response = "Success" });
            //}
            //return Json(new { Response = "Error: User Level is not valid." });
        }

        [HttpDelete]
        public IActionResult UserLevel(int id)
        {
            var userLevel = _context.UserLevel.Find(id);
            if (userLevel != null)
            {
                _context.Entry(userLevel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }

            return Json(new { Response = "Success" });
        }
        #endregion

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

        public IActionResult DataBackup()
        {
            ViewData["Message"] = "Your Data Backup page.";

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
