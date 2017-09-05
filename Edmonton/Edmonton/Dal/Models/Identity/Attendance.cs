using System;
using System.Collections.Generic;

namespace Dal.Models.Identity
{
    public partial class Attendance
    {
        public long AttendanceId { get; set; }
        public long? AssignmentId { get; set; }
        public string Long { get; set; }
        public string Latt { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Assignment Assignment { get; set; }
    }
}
