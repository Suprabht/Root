using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemFrameWork.Email;

namespace WebApp.Models.Home
{
    public class UserDetails
    {
        public String Id { get; set; }  
        public String Name { get; set; }
        public String Email { get; set; }
        public String AlternateEmail { get; set; }
        public String Gender { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public int AlternetPhone { get; set; }
        public String BloodGroup { get; set; }
        public String RoleName { get; set; }
        public int RoleId { get; set; }
        public String LevelsName { get; set; }
        public int LevelsId { get; set; }
        public String Picture { get; set; }
        public int Payment { get; set; }
        public String EffectiveDate { get; set; }
    }
}
