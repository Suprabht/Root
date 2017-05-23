using System;
using System.Collections.Generic;
using System.Text;
using EnumsAndConstants.Enums;

namespace DomainModel.DataContracts
{
    public interface IProgramDetail
    {
        IProgram Program { get; set; }
        LevelsType Level { get; set; }
    }

}
