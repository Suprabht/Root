using System;
using System.Collections.Generic;

namespace DailyVisitors.DAL.Models
{
    public partial class VisitorDetails
    {
        public long VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string Email { get; set; }
        public long MobileNumber { get; set; }
        public string Adress { get; set; }
        public string FromPlace { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public string Picture { get; set; }
        public string Signature { get; set; }
        public string Company { get; set; }
        public int? UserId { get; set; }
        public bool? IsDeleted { get; set; }
        public string PersonInSdl { get; set; }
        public virtual Users User { get; set; }
    }
}
