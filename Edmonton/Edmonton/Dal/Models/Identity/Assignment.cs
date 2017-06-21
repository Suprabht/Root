using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Models.Identity
{
    public partial class Assignment
    {
        public long AssignmentId { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public int? ClientId { get; set; }
        public string UserId { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
