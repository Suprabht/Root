using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Home
{
    public class Leave
    {
        public long LeaveId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public DateTime? LeaveDateFrom { get; set; }
        public DateTime? LeaveDateTo { get; set; }
        public string UserId { get; set; }
    }
}
