using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.DataContracts
{
    public interface IEmployee : IUser
    {
        List<IProgramDetail> ProgramDetails { get; set; }
        List<ITask> Tasks { get; set; }
        IPayment Payment { get; set; }
    }
}
