using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemFrameWork.Error
{
    public enum ErrorType
    {
        ServerSideValidation = 0,

        ServerSideRuntime = 1,

        ClientSideValidation = 2,

        ClientSideRuntime = 3
    }
}
