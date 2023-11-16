using System;
using System.Collections.Generic;

namespace DailyVisitors.DAL.Models
{
    public partial class
        Users
    {
        public Users()
        {
            VisitorDetails = new HashSet<VisitorDetails>();
        }

        public int UserId { get; set; }
        public int EmpowerUserId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string IdentityName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Active { get; set; }
        public int OfficeId { get; set; }

        public virtual ICollection<VisitorDetails> VisitorDetails { get; set; }
    }
}
