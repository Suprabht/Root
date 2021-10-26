using System;
using System.Collections.Generic;

namespace DailyVisitors.DAL.Models
{
    public partial class Office
    {
        public int OfficeId { get; set; }
        public Int16 EmpOfficeId { get; set; }
        public string OfficeName { get; set; }
        public Int16 CountryId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string OUName { get; set; }
        public bool Active { get; set; }
        public string CountryName { get; set; }
    }
}
