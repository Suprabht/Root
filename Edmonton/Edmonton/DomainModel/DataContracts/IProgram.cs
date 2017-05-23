using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.DataContracts
{
    public interface IProgram
    {
        String ProgramName { get; set; }
        String ProgramDescription { get; set; }
    }

}
