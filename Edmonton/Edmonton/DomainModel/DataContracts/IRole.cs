using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.DataContracts
{
    public interface IRole
    {
        string RoleName { get; set; }
        string Description { get; set; }
    }
}
