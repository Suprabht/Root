using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Models.Identity
{
    public partial class Assignment
    {
        public Assignment()
        {
            Attendance = new HashSet<Attendance>();
        }

        public long AssignmentId { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public int? ClientId { get; set; }
        public string UserId { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsAccepted { get; set; }
        public int? NoOfHours { get; set; }

        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual ClientDetails Client { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
