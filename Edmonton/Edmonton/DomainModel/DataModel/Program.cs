using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.DataContracts;

namespace DomainModel.DataModel
{
  
    public class Program : IProgram
    {
        public String ProgramName { get; set; }
        public String ProgramDescription { get; set; }  
    }
}
