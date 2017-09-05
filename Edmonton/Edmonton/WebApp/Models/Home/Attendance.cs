using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Home
{
    public class Attendance
    {
        public long AttendanceId { get; set; }
        public DateTime LogTime { get; set; }
        public string Latt { get; set; }
        public string Long { get; set; }
        public long? AssignmentId { get; set; }
        public string AssignmentDetail { get; set; }
        public DateTime AssignmentDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientLatt { get; set; }
        public string ClientLong { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public double Distance { get; set; }
    }
}
