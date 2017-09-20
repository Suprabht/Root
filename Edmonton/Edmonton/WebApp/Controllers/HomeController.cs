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
                    Email = user.Email,
                    AlternateEmail = user.AlternetEmail,
                    Address = user.Address,
                    Phone = user.PhoneNumber,
                    AlternetPhone = user.AlternetPhone,
                    BloodGroup = user.BloodGroup,
                    RoleId = roll.Id,
                    RoleName = roll.Name
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
            
        }

        public IActionResult EmployeeAttendance(string id)
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Client(int id, Client client)
        {
            try
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
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
        }

        [HttpPut]
        public IActionResult Client(Client client)
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
        public IActionResult TaskAssignment(int id, Models.Home.Assignment programDetails)
        {
            try
            {
                //var assignment = _context.Assignment.Find(id);
                //if (assignment != null)
                //{
                //    assignment.IsAccepted = true;
                //    _context.Assignment.Update(assignment);
                //    _context.SaveChanges();
                //}
                return Json(new { Response = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" + ex.Message });
            }
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
                var leaves = _context.Leave.ToList().Where(u => u.UserId == Encoding.ASCII.GetString(useridB)).ToList();
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

        #region DirectorIndex

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
                assignment = assignmentsDal.Where(x => x.AssignmentDate == DateTime.Today).Single();
                client = _context.ClientDetails.Find(assignment.ClientId);
                return Json(new { isPendingAccept = assignmentsDal.Where(x => x.IsAccepted != true).Count(), clientName = (client.ClientName != null) ? client.ClientName : string.Empty, clientAddress = client.ClientAddress, clientId = assignment.ClientId, assignmentDate = assignment.AssignmentDate });
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

        public IActionResult DataBackup()
        {
            ViewData["Message"] = "Your Data Backup page.";

            return View();
        }


        
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
