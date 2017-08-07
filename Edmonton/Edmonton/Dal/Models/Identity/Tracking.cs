using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class Tracking
    {
        public long TrackingId { get; set; }
        public string UserId { get; set; }
        public string Long { get; set; }
        public string Latt { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
