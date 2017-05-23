using System;
using System.Collections.Generic;
using System.Text;
using DomainModel.DataContracts;

namespace DomainModel.DataModel
{
    public class Hr : IHr
    {
        public IPayment Payment { get; set; }
    }
}
