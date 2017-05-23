using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.ResponseContracts
{
    public interface IBaseResponse
    {
        #region Properties
        bool IsSucceeded { get; set; }

        IList<IValidationFailure> Errors { get; set; }
        #endregion
    }
}
