using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.DataContracts
{
    public interface IUser
    {
        String Name { get; set; }
        String Email { get; set; }
        String AlternateEmail { get; set; }
        String Gender { get; set; }
        String Address { get; set; }
        int Phone { get; set; }
        int AlternatePhone { get; set; }
        String BloodGroup { get; set; }
        String Levels { get; set; }
        String Picture { get; set; }

    }
}
