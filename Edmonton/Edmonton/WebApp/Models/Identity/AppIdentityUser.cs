using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Identity
{
    public class AppIdentityUser : IdentityUser
    {
        public string FullName { get; set; }
        public string SecondName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string AlternetEmail { get; set; }
        public string Address { get; set; }
        public string AlternetPhone { get; set; }
        public string BloodGroup { get; set; }
        public int? UserLevelId { get; set; }
        public string MiddleName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Code { get; set; }
        public string CompensationType { get; set; }
        public int? Rate { get; set; }
    }
}
