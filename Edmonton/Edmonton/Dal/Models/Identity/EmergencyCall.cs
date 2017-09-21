using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Models.Identity
{
    public partial class EmergencyCall
    {
        public long EmergencyCallId { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
