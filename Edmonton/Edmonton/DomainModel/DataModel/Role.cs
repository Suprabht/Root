using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.DataContracts;

namespace DomainModel.DataModel
{
    class Role:IRole
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
