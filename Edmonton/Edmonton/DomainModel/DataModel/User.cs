using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.DataContracts;

namespace DomainModel.DataModel
{
   public class User : IUser
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public String AlternateEmail { get; set; }
        public String Gender { get; set; }
        public String Address { get; set; }
        public int Phone { get; set; }
        public int AlternatePhone { get; set; }
        public String BloodGroup { get; set; }
        public String Levels { get; set; }
        public String Picture { get; set; }
        public Decimal Payment { get; set; }
        public DateTime EffectiveDateTime { get; set; }
    }
}
