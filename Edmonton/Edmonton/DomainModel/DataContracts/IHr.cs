using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.DataContracts
{
    public interface IHr
    {
        IPayment Payment { get; set; }
    }
}
