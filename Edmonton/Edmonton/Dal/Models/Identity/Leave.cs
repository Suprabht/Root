using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class Leave
    {
        public long LeaveId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }

        public string LeaveDescription { get; set; }
    }
}
