using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.DataContracts;

namespace DomainModel.DataModel
{
    public class Director:IUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int AlternatePhone { get; set; }
        public string BloodGroup { get; set; }
        public string Levels { get; set; }
        public string Picture { get; set; }
    }
}
