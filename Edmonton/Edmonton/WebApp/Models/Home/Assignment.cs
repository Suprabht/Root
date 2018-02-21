using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Home
{
    public class Assignment
    {
        public long AssignmentId { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? AssignmentDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string Latt { get; set; }
        public string Long { get; set; }
        public string Link { get; set; }
        public string Accept { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsActive { get; set; }
    }
}
