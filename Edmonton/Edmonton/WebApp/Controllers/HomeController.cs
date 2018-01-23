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
using System.Text;
using GeoCoordinatePortable;
using WebApp.ViewModels.Account;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using OfficeOpenXml;
using System.Globalization;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace WebApp.Controllers
{
    [WhitespaceFilter]
    public class HomeController : Controller
    {
      //  private readonly UserManager<AppIdentityUser> userManager;
        private Appsettings _configuration;
        private readonly BridgeToCareContext _context;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(UserManager<AppIdentityUser> userManager, IOptions<Appsettings> configuration, BridgeToCareContext context, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration.Value;
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public bool sendSms(string body, string mobileNo)
        {
            try
            {
                // Find your Account Sid and Auth Token at twilio.com/console
                string accountSid = _configuration.SMSaccountSid ;
                string authToken = _configuration.SMSauthToken;
                TwilioClient.Init(accountSid, authToken);
                //mobileNo = "+918904007370";
                var to = new PhoneNumber(mobileNo);
                var message = MessageResource.Create(
                    to,
                    from: new PhoneNumber("+15874003767"),
                    body: body);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


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
            try
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
                        users.Add(new UserDetails
                        {
                            Id = user.Id,
                            Name = user.FullName,
                            SecondName = user.SecondName,
                            Email = user.Email,
                            Address = user.Address,
                            AlternateEmail = user.AlternetEmail,
                            AlternetPhone = user.AlternetPhone,
                            BloodGroup = user.BloodGroup,
                            Phone = user.PhoneNumber,
                            MiddleName = user.MiddleName,
                            EmployeeNumber = user.EmployeeNumber,
                            Code = user.Code,
                            CompensationType = user.CompensationType,
                            Rate = user.Rate
                        });
                    }
                }
                return Json(new { page = 1, records = users.Count, rows = users });

            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
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
            try
            {
                //var user = _context.AspNetUsers.ToList().Where(n => n.Id == id).ToList()[0];
                var user = _context.AspNetUsers.Find(id);
                var userRoll = _context.AspNetUserRoles.ToList().Where(ur => ur.UserId == id).ToList()[0];
                var roll = _context.AspNetRoles.ToList().Where(r => r.Id == userRoll.RoleId).Single();
                UserDetails userDetails = new UserDetails
                {
                    Id = user.Id,
                    Name = user.FullName,
                    SecondName = user.SecondName,
                    Email = user.Email,
                    AlternateEmail = user.AlternetEmail,
                    Address = user.Address,
                    Phone = user.PhoneNumber,
                    AlternetPhone = user.AlternetPhone,
                    BloodGroup = user.BloodGroup,
                    RoleId = roll.Id,
                    RoleName = roll.Name,
                    MiddleName = user.MiddleName,
                    EmployeeNumber = user.EmployeeNumber,
                    Code = user.Code,
                    CompensationType = user.CompensationType,
                    Rate = user.Rate
                };

                return ViewComponent("UserDetails", userDetails);
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        public IActionResult AddUser(string id)
        {
            try
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpPost]
        public IActionResult AddUserDetails(UserDetails userDetails)
        {
            try
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
                user.SecondName = userDetails.SecondName;
                user.AlternetEmail = userDetails.AlternateEmail;
                user.Address = userDetails.Address;
                user.AlternetPhone = userDetails.AlternetPhone;
                user.BloodGroup = userDetails.BloodGroup;

                user.MiddleName = userDetails.MiddleName;
                user.EmployeeNumber = userDetails.EmployeeNumber;
                user.Code = userDetails.Code;
                user.CompensationType = userDetails.CompensationType;
                user.Rate = userDetails.Rate;
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpPost]
        public IActionResult DeleteUserDetails(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }
                var userToDelete = _context.AspNetUsers.Find(id);
                _context.Entry(userToDelete).State = EntityState.Deleted;
                _context.SaveChanges();

                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }

        [HttpPost]
        public IActionResult UpdateUserDetails(UserDetails userDetails)
        {
            try
            {
                if (userDetails == null)
                {
                    return NotFound();
                }
                var userToUpdate = _context.AspNetUsers.Find(userDetails.Id);
                userToUpdate.PhoneNumber = userDetails.Phone;
                userToUpdate.Email = userDetails.Email;
                userToUpdate.FullName = userDetails.Name;
                userToUpdate.SecondName = userDetails.SecondName;
                userToUpdate.AlternetEmail = userDetails.AlternateEmail;
                userToUpdate.Address = userDetails.Address;
                userToUpdate.AlternetPhone = userDetails.AlternetPhone;
                userToUpdate.BloodGroup = userDetails.BloodGroup;
                userToUpdate.MiddleName = userDetails.MiddleName;
                userToUpdate.EmployeeNumber = userDetails.EmployeeNumber;
                userToUpdate.Code = userDetails.Code;
                userToUpdate.CompensationType = userDetails.CompensationType;
                userToUpdate.Rate = userDetails.Rate;
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        public IActionResult EmployeeAttendance(string id)
        {
            try
            {
                if(string.IsNullOrEmpty(id))
                {
                    byte[] useridB;
                    var userId = HttpContext.Session.TryGetValue("userId", out useridB);
                    id =  Encoding.ASCII.GetString(useridB);
                }

                List<Models.Home.Attendance> assigns = new List<Models.Home.Attendance>();
                var attendanceDal = _context.Attendance.ToList();
                foreach (var atteDal in attendanceDal)
                {
                    Models.Home.Attendance attend = new Models.Home.Attendance();
                    attend.AttendanceId = atteDal.AttendanceId;
                    attend.Latt = atteDal.Latt;
                    attend.Long = atteDal.Long;

                    var assignment = _context.Assignment.Find(atteDal.AssignmentId);

                    attend.AssignmentId = atteDal.AssignmentId;
                    attend.AssignmentDate = Convert.ToDateTime(assignment.AssignmentDate);
                    attend.ClientId = Convert.ToInt16(assignment.ClientId);
                    var client = _context.ClientDetails.Find(attend.ClientId);
                    attend.ClientName = client.ClientName;
                    attend.ClientAddress = client.ClientAddress;

                    attend.UserId = assignment.UserId;
                    var user = _context.AspNetUsers.Find(assignment.UserId);
                    attend.UserName = user.UserName;
                    attend.UserEmail = user.Email;

                    attend.AssignmentDetail = String.Format("{2}. Client Name: {0} Client Address: {1}<span class'approved'> {3} </span>", client.ClientName, client.ClientAddress, atteDal.AssignmentId, (assignment.IsApproved??false)?"Approved!!":"Not Approved!!");

                    var sCoord = new GeoCoordinate(Convert.ToDouble(client.Latt), Convert.ToDouble(client.Long));
                    var eCoord = new GeoCoordinate(Convert.ToDouble(atteDal.Latt), Convert.ToDouble(atteDal.Long));

                    attend.Distance = sCoord.GetDistanceTo(eCoord);

                    attend.LogTime = Convert.ToDateTime(atteDal.CreatedOn);
                    assigns.Add(attend);
                    assigns = assigns.ToList().Where(x => x.UserId == id).ToList();
                }
                var jsonstring = JsonHelper.Serialize(assigns);
                return Json(new { page = 1, records = attendanceDal.Count, rows = assigns });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateAttendenceAccept(long assignmentId, int noOfHours)
        {
            try
            {
                var assignment = _context.Assignment.Find(assignmentId);
                assignment.NoOfHours = noOfHours;
                assignment.IsApproved = true;
                _context.Assignment.Update(assignment);
                _context.SaveChanges();

                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }
        /*
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public IActionResult ForgetPasswordByUserEmail(string userEmail)
        {
            try
            {
                if (string.IsNullOrEmpty(userEmail))
                {
                    return NotFound();
                }
                const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                StringBuilder res = new StringBuilder();
                Random rnd = new Random();
                int length = 7;
                while (0 < length--)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                }
                var password = res.ToString() + "123!";

                //var userById = _context.AspNetUsers.Find(userId);
                ForgetPasswordViewModel obj = new ForgetPasswordViewModel
                {
                    EmailId = userEmail,
                    Password = password
                };

                var user = userManager.FindByEmailAsync(userEmail);
                if (user.Result != null)
                {
                    var token = userManager.GeneratePasswordResetTokenAsync(user.Result).Result;
                    if (!string.IsNullOrEmpty(token))
                    {
                        IdentityResult result = userManager.ResetPasswordAsync(user.Result, token, obj.Password).Result;
                        if (result.Succeeded)
                        {
                            return Json(new { Response = "Success" });
                        }
                    }

                }
                return Json(new { Response = "Failed!" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }
        */
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
            try
            {
                var programs = _context.ProgramDetails.ToList();
                var jsonstring = JsonHelper.Serialize(programs);
                return Json(new { page = 1, records = programs.Count, rows = programs });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpPost]
        public IActionResult Program(int id, ProgramDetails programDetails)
        {
            try
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpPut]
        public IActionResult Program(ProgramDetails programDetails)
        {
            try
            {
                _context.ProgramDetails.Add(programDetails);
                _context.SaveChanges();
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
           
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

        #region Programs
        public IActionResult Program1()
        {
            try
            {
                var programsDal = _context.Program.ToList();
                List<Models.Home.Program> programs = new List<Models.Home.Program>();
                foreach (var programDal in programsDal)
                {
                    var program = new Models.Home.Program();
                    program.ProgramId = programDal.ProgramId;
                    program.ProgramCode = programDal.ProgramCode;
                    program.ProgramName = programDal.ProgramName;
                    program.ProgramDescription = programDal.ProgramDescription;
                    program.ProgramCategoryId = programDal.ProgramCategoryId;
                    var programCategory = _context.ProgramCategory.ToList().Where(x => x.ProgramCategoryId == program.ProgramCategoryId).Single();
                    programDal.ProgramCategory = programCategory;
                    program.ProgramCategoryCode = programDal.ProgramCategory.ProgramCategoryCode;
                    program.ProgramCategoryName = programDal.ProgramCategory.ProgramCategoryName;
                    program.ProgramCategoryAbbreviation = programDal.ProgramCategory.ProgramCategoryAbbreviation;
                    programs.Add(program);
                }
                return Json(new { page = 1, records = programs.Count, rows = programs });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }
        [HttpPost]
        public IActionResult Program1(int id, Models.Home.Program program)
        {
            try
            {
                if (program == null)
                {
                    return NotFound();
                }
                var programToUpdate = _context.Program.Find(program.ProgramId);
                programToUpdate.ProgramName = program.ProgramName;
                programToUpdate.ProgramCode = program.ProgramCode;
                programToUpdate.ProgramDescription = program.ProgramDescription;
                programToUpdate.ProgramCategoryId = program.ProgramCategoryId;
                _context.Program.Update(programToUpdate);
                _context.SaveChanges();
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }

        [HttpPut]
        public IActionResult Program1(Models.Home.Program program)
        {
            try
            {
                if (program == null)
                {
                    return NotFound();
                }
                var programDal = new Dal.Models.Identity.Program();
                programDal.ProgramName = program.ProgramName;
                programDal.ProgramCode = program.ProgramCode;
                programDal.ProgramDescription = program.ProgramDescription;
                programDal.ProgramCategoryId = program.ProgramCategoryId;
                _context.Program.Add(programDal);
                _context.SaveChanges();
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }
        [HttpDelete]
        public IActionResult Program1(int id)
        {
            var program = _context.Program.Find(id);
            if (program != null)
            {
                _context.Entry(program).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return Json(new { Response = "Success" });
        }
        #endregion

        #region ProgramCategory
        [Authorize]
        public IActionResult ProgramCategorys()
        {
            ViewData["Message"] = "Your Program Details page.";

            return View();
        }

        [HttpGet]
        public IActionResult ProgramCategory()
        {
            try
            {
                var programsCategorys = _context.ProgramCategory.ToList();
                return Json(new { page = 1, records = programsCategorys.Count, rows = programsCategorys });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }
        [HttpPost]
        public IActionResult ProgramCategory(int id, ProgramCategory programCategory)
        {
            try
            {
                if (programCategory == null)
                {
                    return NotFound();
                }
                var programCategoryToUpdate = _context.ProgramCategory.Find(programCategory.ProgramCategoryId);
                programCategoryToUpdate.ProgramCategoryName = programCategory.ProgramCategoryName;
                programCategoryToUpdate.ProgramCategoryDescription = programCategory.ProgramCategoryDescription;
                programCategoryToUpdate.ProgramCategoryAbbreviation = programCategory.ProgramCategoryAbbreviation;
                programCategoryToUpdate.ProgramCategoryCode = programCategory.ProgramCategoryCode;
                _context.ProgramCategory.Update(programCategoryToUpdate);
                _context.SaveChanges();
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }

        [HttpPut]
        public IActionResult ProgramCategory(ProgramCategory programCategory)
        {
            try
            {
                _context.ProgramCategory.Add(programCategory);
                _context.SaveChanges();
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }
        [HttpDelete]
        public IActionResult ProgramCategory(int id)
        {
            var programCategory = _context.ProgramCategory.Find(id);
            if (programCategory != null)
            {
                _context.Entry(programCategory).State = EntityState.Deleted;
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
            try
            {
                var clientsDal = _context.ClientDetails.ToList();
                List<Models.Home.Client> clients = new List<Models.Home.Client>();
                foreach (var clientDal in clientsDal)
                {
                    var client = new Models.Home.Client();
                    client.ClientAddress = clientDal.ClientAddress;
                    client.ClientId = clientDal.ClientId;
                    client.ClientName = clientDal.ClientName;
                    client.Latt = clientDal.Latt;
                    client.Long = clientDal.Long;
                    client.Link = string.Format("http://maps.google.com/maps?q={0},{1}", clientDal.Latt, clientDal.Long);
                    var program = _context.Program.ToList().Where(x => x.ProgramId == clientDal.ProgramId).Single();
                    clientDal.Program = program;
                    var programCategory = _context.ProgramCategory.ToList().Where(x => x.ProgramCategoryId == program.ProgramCategoryId).Single();
                    clientDal.Program.ProgramCategory = programCategory;
                    client.ProgramId = clientDal.Program.ProgramId;
                    client.ProgramCode = clientDal.Program.ProgramCode;
                    client.ProgramName = clientDal.Program.ProgramName;
                    client.ProgramCategoryId = clientDal.Program.ProgramCategory.ProgramCategoryId;
                    client.ProgramCategoryCode = clientDal.Program.ProgramCategory.ProgramCategoryCode;
                    client.ProgramCategoryName = clientDal.Program.ProgramCategory.ProgramCategoryName;
                    client.ProgramCategoryAbbreviation = clientDal.Program.ProgramCategory.ProgramCategoryAbbreviation;
                    //client.Program = string.Format("{0}:{1} - {2}:{3}", clientDal.ProgramId, clientDal.Program.ProgramName, clientDal.Program.ProgramCategoryId, clientDal.Program.ProgramCategory.ProgramCategoryName);
                    clients.Add(client);
                }
                return Json(new { page = 1, records = clients.Count, rows = clients });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Client(int id, Models.Home.Client client)
        {
            try
            {
                var clientToUpdate = _context.ClientDetails.Find(client.ClientId);

                clientToUpdate.ClientAddress = client.ClientAddress;
                clientToUpdate.ClientName = client.ClientName;
                clientToUpdate.Latt = client.Latt;
                clientToUpdate.Long = client.Long;
                clientToUpdate.ProgramId = client.ProgramId;
                _context.ClientDetails.Update(clientToUpdate);
                _context.SaveChanges();
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }

        [HttpPut]
        public IActionResult Client(Models.Home.Client client)
        {
            try
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpDelete]
        public IActionResult Client(int id)
        {
            try
            {
                var clientDetails = _context.ClientDetails.Find(id);
                if (clientDetails != null)
                {
                    _context.Entry(clientDetails).State = EntityState.Deleted;
                    _context.SaveChanges();
                }
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
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
            try
            {
                List<Models.Home.Assignment> assigns = new List<Models.Home.Assignment>();
                var assignmentsDal = _context.Assignment.ToList();
                foreach (var assignmentDal in assignmentsDal)
                {
                    Models.Home.Assignment assign = new Models.Home.Assignment();
                    assign.AssignmentId = assignmentDal.AssignmentId;
                    assign.AssignmentDate = Convert.ToDateTime(assignmentDal.AssignmentDate);
                    assign.Accept = Convert.ToString(assignmentDal.IsAccepted);
                    //var client = new ClientDetails();
                    if (assignmentDal.ClientId != null)
                    {
                        assign.ClientId = Convert.ToInt16(assignmentDal.ClientId);
                        var client = _context.ClientDetails.Find(assign.ClientId);
                        assign.ClientName = client.ClientName;
                        assign.ClientAddress = client.ClientAddress;
                        assign.UserId = assignmentDal.UserId;
                    }
                    
                    if (assign.UserId != null)
                    {
                        var user = _context.AspNetUsers.Find(assign.UserId);
                        assign.UserName = user.UserName;
                        assign.UserEmail = user.Email;                        
                    }                    
                    assigns.Add(assign);
                }
                var jsonstring = JsonHelper.Serialize(assigns);
                return Json(new { page = 1, records = assignmentsDal.Count, rows = assigns });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpPost]
        public IActionResult TaskAssignment(int id, Models.Home.Assignment assignment)
        {
            try
            {
                var assign = _context.Assignment.Find(assignment.AssignmentId);
                if (assignment != null)
                {
                    assign.ClientId = Convert.ToInt32(assignment.ClientId);
                    assign.UserId = assignment.UserId;
                    assign.IsAccepted = null;
                    assign.AssignmentDate = assignment.AssignmentDate;
                    _context.Assignment.Update(assign);
                    _context.SaveChanges();
                    var client = _context.ClientDetails.Where(x => x.ClientId == (Convert.ToInt32(assignment.ClientId))).Single();
                    var user = _context.AspNetUsers.Find(assignment.UserId);
                    if(user.PhoneNumber != null)
                        sendSms("Your assignment has been updated Client Name:" + client.ClientName +", Address:"+ client.ClientAddress + ", Date:" + assignment.AssignmentDate, user.PhoneNumber);
                }
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }

        [HttpPut]
        public IActionResult TaskAssignment(Models.Home.Assignment assignment)
        {
            try
            {
                var assign = new Dal.Models.Identity.Assignment()
                {
                    ClientId = Convert.ToInt32( assignment.ClientName),
                    UserId = assignment.UserName,
                    AssignmentDate = assignment.AssignmentDate,
                    IsAccepted = false
                    //AssignmentId = DBNull.Value
                };
                _context.Assignment.Add(assign);
                _context.SaveChanges();
                var client = _context.ClientDetails.Where(x=>x.ClientId == (Convert.ToInt32(assignment.ClientName))).Single();
                var user = _context.AspNetUsers.Find(assignment.UserName);
                if (user.PhoneNumber != null)
                    sendSms("Your assignment has been Added Client Name:" + client.ClientName + ", Address:" + client.ClientAddress + ", Date:" + assignment.AssignmentDate, user.PhoneNumber);
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult TaskAssignment(long id)
        {
            try
            {
                var assignment = _context.Assignment.Find(id);
                if (assignment != null)
                {
                    _context.Entry(assignment).State = EntityState.Deleted;
                    _context.SaveChanges();
                }
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }
        #endregion

        #region AssignmentPlan
        public IActionResult AssignmentPlan()
        {
            ViewData["Message"] = "Your Assignment Plan page.";

            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult AssignmentPlanRecords()
        {
            try
            {
                List<Models.Home.Assignment> assigns = new List<Models.Home.Assignment>();
                byte[] useridB;
                var userId = HttpContext.Session.TryGetValue("userId", out useridB);
                var assignmentsDal = _context.Assignment.ToList().Where(u => u.UserId == Encoding.ASCII.GetString(useridB)).ToList();
                foreach (var assignmentDal in assignmentsDal)
                {
                    Models.Home.Assignment assign = new Models.Home.Assignment();
                    assign.AssignmentId = assignmentDal.AssignmentId;
                    assign.AssignmentDate = Convert.ToDateTime(assignmentDal.AssignmentDate);
                    assign.Accept = Convert.ToString(assignmentDal.IsAccepted);

                    assign.ClientId = Convert.ToInt16(assignmentDal.ClientId);
                    var client = _context.ClientDetails.Find(assign.ClientId);
                    assign.ClientName = client.ClientName;
                    assign.ClientAddress = client.ClientAddress;
                    assign.Latt = client.Latt;
                    assign.Long = client.Long;
                    assign.Link = string.Format("http://maps.google.com/maps?q={0},{1}", client.Latt, client.Long);

                    assign.UserId = assignmentDal.UserId;
                    var user = _context.AspNetUsers.Find(assign.UserId);
                    assign.UserName = user.UserName;
                    assign.UserEmail = user.Email;
                    assigns.Add(assign);
                }
                var jsonstring = JsonHelper.Serialize(assigns);
                return Json(new { page = 1, records = assignmentsDal.Count, rows = assigns });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
           
        }

        [Authorize]
        [HttpGet]
        public IActionResult AcceptAssignment(long id)
        {
            try
            {
                var assignment = _context.Assignment.Find(id);
                if (assignment != null)
                {
                    assignment.IsAccepted = true;
                    _context.Assignment.Update(assignment);
                    _context.SaveChanges();
                    var client = _context.ClientDetails.Find(Convert.ToInt32(assignment.ClientId));
                    var user = _context.AspNetUsers.Find(assignment.UserId);
                    if (user.PhoneNumber != null)
                        sendSms("Your have accept assignment Client Name:" + client.ClientName + ", Address:" + client.ClientAddress + ", Date:" + assignment.AssignmentDate, user.PhoneNumber);
                }
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult CancelAssignment(long id)
        {
            try
            {
                var assignment = _context.Assignment.Find(id);
                if (assignment != null)
                {
                    assignment.IsAccepted = false;
                    _context.Assignment.Update(assignment);
                    _context.SaveChanges();
                    var client = _context.ClientDetails.Find(Convert.ToInt32(assignment.ClientId));
                    var user = _context.AspNetUsers.Find(assignment.UserId);
                    if (user.PhoneNumber != null)
                        sendSms(user.UserName + " has rejected assignment Client Name:" + client.ClientName, "+15876793536");
                }
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
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
            try
            {
                var programs = _context.UserLevel.ToList();
                var jsonstring = JsonHelper.Serialize(programs);
                return Json(new { page = 1, records = programs.Count, rows = programs });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpPost]
        public IActionResult UserLevel(int id, UserLevel userLevel)
        {
            try
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpPut]
        public IActionResult UserLevel(UserLevel userLevel)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                _context.UserLevel.Add(userLevel);
                _context.SaveChanges();
                return Json(new { Response = "Success" });
                //}
                //return Json(new { Response = "Error: User Level is not valid." });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
           
        }

        [HttpDelete]
        public IActionResult UserLevel(int id)
        {
            try
            {
                var userLevel = _context.UserLevel.Find(id);
                if (userLevel != null)
                {
                    _context.Entry(userLevel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    _context.SaveChanges();
                }

                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
           
        }
        #endregion

        #region EmployeesIndex
        public IActionResult EmployeesIndex()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        #endregion

        #region LeavePlanner
        public IActionResult LeavePlanner()
        {
            ViewData["Message"] = "Your Leave Planner page.";

            return View();
        }

        [HttpGet]
        public IActionResult Leave()
        {
            try
            {
                byte[] useridB;
                var userId = HttpContext.Session.TryGetValue("userId", out useridB);
                var user = Encoding.ASCII.GetString(useridB);
                var leaves = _context.Leave.ToList().Where(u => u.UserId == user).ToList();
                List<Models.Home.Leave> leavesModel = new List<Models.Home.Leave>();
                foreach (var leave in leaves)
                {
                    leavesModel.Add(new Models.Home.Leave
                    {
                        LeaveId = leave.LeaveId,
                        LeaveDate = leave.LeaveDate,
                        UserId = leave.UserId
                    });
                }
                return Json(new { page = 1, records = leavesModel.Count, rows = leavesModel });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        [HttpPut]
        public IActionResult Leave(Dal.Models.Identity.Leave leave)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                byte[] useridB;
                var userIdb = HttpContext.Session.TryGetValue("userId", out useridB);
                var userId = Encoding.ASCII.GetString(useridB);
                leave.UserId = userId;
                _context.Leave.Add(leave);
                _context.SaveChanges();
                return Json(new { Response = "Success" });
                //}
                //return Json(new { Response = "Error: User Level is not valid." });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }

        [HttpDelete]
        public IActionResult Leave(long id)
        {
            try
            {
                var leave = _context.Leave.Find(id);
                if (leave != null)
                {
                    _context.Entry(leave).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    _context.SaveChanges();
                }

                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }

        public IActionResult LeaveCalendar(string id)
        {
            try
            {
                byte[] useridB;
                var userId = HttpContext.Session.TryGetValue("userId", out useridB);
                var leaves = _context.Leave.ToList().Where(u => u.UserId == (string.IsNullOrEmpty(id) ? Encoding.ASCII.GetString(useridB) : id)).ToList();
                List<CalendarDate> calendarDates = new List<CalendarDate>();
                foreach (var leave in leaves)
                {
                    calendarDates.Add(new CalendarDate
                    {
                        Date = String.Format("{0:yyyy-MM-dd}", leave.LeaveDate),
                        Badge = false,
                        Title = leave.LeaveDescription
                        //Body = "<p class=\"lead\">Information for this date</p><p>You can add <strong>html</strong> in this block</p>",
                        //Footer = "Extra information",
                        //Classname = "grade-1"
                    });
                }
                return Json(new { calendarDates });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }
        #endregion

        #region Attendance
        public IActionResult AttendanceDetails()
        {
            ViewData["Message"] = "Your Attendance Details page.";

            return View();
        }

        public IActionResult Attendance()
        {
            try
            {
                List<Models.Home.Attendance> assigns = new List<Models.Home.Attendance>();
                var attendanceDal = _context.Attendance.ToList();
                foreach (var atteDal in attendanceDal)
                {
                    Models.Home.Attendance attend = new Models.Home.Attendance();
                    attend.AttendanceId = atteDal.AttendanceId;
                    attend.Latt = atteDal.Latt;
                    attend.Long = atteDal.Long;

                    var assignment = _context.Assignment.Find(atteDal.AssignmentId);

                    attend.AssignmentId = atteDal.AssignmentId;
                    attend.AssignmentDate = Convert.ToDateTime(assignment.AssignmentDate);
                    attend.ClientId = Convert.ToInt16(assignment.ClientId);
                    var client = _context.ClientDetails.Find(attend.ClientId);
                    attend.ClientName = client.ClientName;
                    attend.ClientAddress = client.ClientAddress;

                    attend.UserId = assignment.UserId;
                    var user = _context.AspNetUsers.Find(assignment.UserId);
                    attend.UserName = user.UserName;
                    attend.UserEmail = user.Email;

                    attend.AssignmentDetail = String.Format("{2}. Client Name: {0} Client Address: {1}", client.ClientName, client.ClientAddress, atteDal.AssignmentId);

                    var sCoord = new GeoCoordinate(Convert.ToDouble(client.Latt), Convert.ToDouble(client.Long));
                    var eCoord = new GeoCoordinate(Convert.ToDouble(atteDal.Latt), Convert.ToDouble(atteDal.Long));

                    attend.Distance = sCoord.GetDistanceTo(eCoord);

                    attend.LogTime = Convert.ToDateTime(atteDal.CreatedOn);
                    assigns.Add(attend);
                }
                var jsonstring = JsonHelper.Serialize(assigns);
                return Json(new { page = 1, records = attendanceDal.Count, rows = assigns });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }
        #endregion

        #region AttendanceView
        public IActionResult AttendanceView()
        {
            ViewData["Message"] = "Your Attendance Details page.";

            return View();
        }
        #endregion

        #region DirectorIndex
        public IActionResult ActiveLogin()
        {
            try
            {
                List<Models.Home.Assignment> assigns = new List<Models.Home.Assignment>();
                
                var assignments = _context.Assignment.ToList();
                foreach (var assignmentDal in assignments)
                {
                    if(assignmentDal.IsActive??false)
                    {
                        if(assignmentDal.AssignmentDate == DateTime.Today)
                        {
                            Models.Home.Assignment assignment = new Models.Home.Assignment();

                            assignment.AssignmentId = assignmentDal.AssignmentId;
                            assignment.AssignmentDate = Convert.ToDateTime(assignmentDal.AssignmentDate);
                            assignment.ClientId = Convert.ToInt16(assignmentDal.ClientId);
                            var client = _context.ClientDetails.Find(assignmentDal.ClientId);
                            assignment.ClientName = client.ClientName;
                            assignment.ClientAddress = client.ClientAddress;

                            assignment.UserId = assignment.UserId;
                            var user = _context.AspNetUsers.Find(assignmentDal.UserId);
                            assignment.UserName = user.UserName;
                            assignment.UserEmail = user.Email;
                            assignment.IsActive = assignmentDal.IsActive ?? false;

                            assigns.Add(assignment);
                        }                        
                    }

                }
                var jsonstring = JsonHelper.Serialize(assigns);
                return Json(new { page = 1, records = assigns.Count, rows = assigns });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Response = "Error" + ex.Message
                });
            }
        }

        public IActionResult ActiveEmergencyCall()
        {
            try
            {
                List<Models.Home.Assignment> assigns = new List<Models.Home.Assignment>();

                var emergencyCalls = _context.EmergencyCall.ToList();
                foreach (var emergencyCall in emergencyCalls)
                {
                    if (emergencyCall.IsActive ?? false)
                    {
                        Models.Home.Assignment assignment = new Models.Home.Assignment();                       

                        assignment.UserId = emergencyCall.UserId;
                        var user = _context.AspNetUsers.Find(emergencyCall.UserId);
                        assignment.UserName = user.UserName;
                        assignment.UserEmail = user.Email;
                        assignment.IsActive = emergencyCall.IsActive ?? false;

                        assigns.Add(assignment);
                    }

                }
                var jsonstring = JsonHelper.Serialize(assigns);
                return Json(new { page = 1, records = assigns.Count, rows = assigns });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Response = "Error" + ex.Message
                });
            }
        }


        public IActionResult IndexDashBoard()
        {
            try
            {
                var assignment = _context.Assignment.ToList();

                return Json(new { isAccepted = assignment.Where(x => x.IsAccepted == (true)).Count(), pendingApproval = assignment.Where(x => x.IsApproved == null).Count(), pendingAcceptence = assignment.Where(x => x.IsAccepted == null).Count() });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }
        #endregion

        #region EmployeIndex
        public IActionResult IndexDashBoardEmployes()
        {
            
            try
            {
                byte[] useridB;
                var userId = HttpContext.Session.TryGetValue("userId", out useridB);
                var assignmentsDal = (useridB == null) ? null : _context.Assignment.ToList().Where(u => u.UserId == Encoding.ASCII.GetString(useridB)).ToList();
                var assignment = new Dal.Models.Identity.Assignment();
                var client = new ClientDetails();
                var assignmentList = assignmentsDal.Where(x => x.AssignmentDate == DateTime.Today).ToList();
                if (assignmentList.Count > 0)
                {
                    assignment = assignmentList.Single();
                    // .Single();
                    client = _context.ClientDetails.Find(assignment.ClientId);
                    return Json(new
                    {
                        isPendingAccept = assignmentsDal.Where(x => x.IsAccepted != true).Count(),
                        clientName = (client.ClientName != null) ? client.ClientName : string.Empty,
                        clientAddress = client.ClientAddress,
                        clientId = assignment.ClientId,
                        assignmentDate = assignment.AssignmentDate,
                        assignmentId = assignment.AssignmentId
                    });
                }
                return Json(new
                {
                    isPendingAccept = assignmentsDal.Where(x => x.IsAccepted != true).Count()
                });

            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        public IActionResult ActivateLogin(long id)
        {
            try
            {
                if (id != 0)
                {
                    var assignment = _context.Assignment.Find(id);
                    assignment.IsActive = true;
                    _context.Assignment.Update(assignment);
                    _context.SaveChanges();
                    return Json(new { Response = "Success" });
                }
                return Json(new { Response = "Error Assignment not set" });

            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }

        public IActionResult EmergencyLogin()
        {
            try
            {
                HttpContext.Session.Set("EmergencyLogin", Encoding.ASCII.GetBytes(true.ToString()));
                byte[] useridB;
                var user = HttpContext.Session.TryGetValue("userId", out useridB);
                string userId = Encoding.ASCII.GetString(useridB);

                if (!string.IsNullOrEmpty(userId))
                {
                    EmergencyCall emergencyCall = new EmergencyCall();
                    emergencyCall.UserId = userId;
                    emergencyCall.IsActive = true;
                    emergencyCall.LoginTime = System.DateTime.UtcNow;
                    _context.EmergencyCall.Add(emergencyCall);
                    _context.SaveChanges();
                    return Json(new { Response = "Success" });
                }
                return Json(new { Response = "Error Assignment not set" });

            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }

        }
        #endregion

        public IActionResult UserInfo()
        {
            ViewData["Message"] = "Your User Info page.";

            return View();
        }
        #region DataBackup
        [Authorize]
        public IActionResult DataBackup()
        {
            ViewData["Message"] = "Your Data Backup page.";
            ViewData["Test"] = false;
            return View();
        }
        [HttpGet]
        public IActionResult ExportToExcel()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"data.xlsx";
            string URL = string.Format("{0}://{1}/wwwroot/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee");
                //First add the headers
                worksheet.Cells[1, 1].Value = "First Name";
                worksheet.Cells[1, 2].Value = "Middle Initial";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Employee Number";
                worksheet.Cells[1, 5].Value = "Code";
                worksheet.Cells[1, 6].Value = "Type";
                worksheet.Cells[1, 7].Value = "Name";
                worksheet.Cells[1, 8].Value = "Payment No";
                worksheet.Cells[1, 9].Value = "Compensation Type";
                worksheet.Cells[1, 10].Value = "Attribute 1";
                worksheet.Cells[1, 11].Value = "Value 1";
                worksheet.Cells[1, 12].Value = "Attribute 2";
                worksheet.Cells[1, 13].Value = "Value 2";
                worksheet.Cells[1, 14].Value = "Attribute 3";
                worksheet.Cells[1, 15].Value = "Value 3";
                worksheet.Cells[1, 16].Value = "Tax Frequency";

                var userRolls = _context.AspNetUserRoles.ToList().Where(ur => ur.RoleId == "105e66c8-4495-48f3-98e6-7b9178537168").ToList();
                var row = 2;
                foreach (var userRoll in userRolls)
                {
                    var user = _context.AspNetUsers.Where(u => u.Id == userRoll.UserId).Single();
                    if (user.Id != "7b081709-6e73-4cba-8950-d3ae92a8a387")
                    {
                        String strDate = DateTime.Now.ToString("MM/yyyy");
                        DateTime fromDate = DateTime.ParseExact(("26-" + strDate).Replace('-', '/'), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        fromDate = fromDate.AddMonths(-1);

                        var assignments = _context.Assignment.Where(a => a.UserId == user.Id).Where(a => a.IsApproved == true).Where(a => a.AssignmentDate >= fromDate).ToList();
                        var totalHours = 0;
                        foreach (var assignment in assignments)
                        {
                            totalHours = totalHours + (assignment.NoOfHours) ?? 0;
                        }

                        worksheet.Cells[row, 1].Value = user.FullName;
                        worksheet.Cells[row, 2].Value = user.MiddleName;
                        worksheet.Cells[row, 3].Value = user.SecondName;
                        worksheet.Cells[row, 4].Value = user.EmployeeNumber;
                        worksheet.Cells[row, 5].Value = user.Code;
                        worksheet.Cells[row, 6].Value = "Earning";
                        worksheet.Cells[row, 7].Value = "Regular";
                        worksheet.Cells[row, 8].Value = "1";
                        worksheet.Cells[row, 9].Value = user.CompensationType;
                        worksheet.Cells[row, 10].Value = "Hours";
                        worksheet.Cells[row, 11].Value = totalHours.ToString();
                        worksheet.Cells[row, 12].Value = "Rate";
                        worksheet.Cells[row, 13].Value = user.Rate;
                        worksheet.Cells[row, 14].Value = "Amount";
                        worksheet.Cells[row, 15].Value = (user.Rate * totalHours).ToString();
                        worksheet.Cells[row, 16].Value = "26 - Bi-Weekly";
                        row++;
                    }
                    

                }
                
                package.Save(); //Save the workbook.
            }
            
            ViewData["Test"] = true;
            return View("DataBackup");
        }
        #endregion


        public IActionResult PaymentView()
        {
            ViewData["Message"] = "Your Payment View page.";

            return View();
        }
        

        public IActionResult Error()
        {
            return View();
        }
    }
}
