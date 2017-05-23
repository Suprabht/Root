using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.DataContracts;

namespace DomainModel.DataModel
{
   
    public class Task : ITask
    {
        public String CustomerName { get; set; }
        public String CustomerAddress { get; set; }
        public IProgram Program { get; set; }
        public IEmployee AssignToEmployees { get; set; }
        public bool IsAssigned => (AssignToEmployees == null);
    }
}
