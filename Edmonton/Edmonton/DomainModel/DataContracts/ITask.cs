using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.DataContracts
{
    public interface ITask
    {
        String CustomerName { get; set; }
        String CustomerAddress { get; set; }
        IProgram Program { get; set; }
        IEmployee AssignToEmployees { get; set; }
        bool IsAssigned { get; }
    }
}
