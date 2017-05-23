using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.ResponseContracts
{
    public interface IValidationFailure
    {
        #region Properties
        string ErrorMessage { get; set; }
        string PropertyName { get; set; }
        #endregion
    }
}
