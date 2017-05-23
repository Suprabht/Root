using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;
using DomainModel.DataContracts;
using EnumsAndConstants.Enums;

namespace DomainModel.DataModel
{
    
    public class ProgramDetail : IProgramDetail
    {
        public IProgram Program { get; set; }
        public LevelsType Level { get; set; }
    }
}
