using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.ResponseContracts
{
    public interface IResult:IBaseResponse
    {
        object Result { get; set; }
    }
}
