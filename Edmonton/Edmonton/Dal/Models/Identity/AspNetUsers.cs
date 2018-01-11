using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            Assignment = new HashSet<Assignment>();
            EmergencyCall = new HashSet<EmergencyCall>();
            Leave = new HashSet<Leave>();
            Tracking = new HashSet<Tracking>();
        }

        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
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

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<Assignment> Assignment { get; set; }
        public virtual ICollection<EmergencyCall> EmergencyCall { get; set; }
        public virtual ICollection<Leave> Leave { get; set; }
        public virtual ICollection<Tracking> Tracking { get; set; }
        public virtual UserLevel UserLevel { get; set; }
    }
}
