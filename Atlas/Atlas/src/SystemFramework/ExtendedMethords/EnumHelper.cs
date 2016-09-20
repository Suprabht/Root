

namespace SystemFrameWork.ExtendedMethords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumHelper
    {
        #region Private Variable

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region private methods

        #endregion

        #region Public methods

        /// <summary>
        /// Extended method to convert enum to dictionary
        /// </summary>
        /// <param name="objectEnum"></param>
        /// <returns>Dictionary</returns>
        public static Dictionary<int, string> ToDictionary(this Enum objectEnum)
        {
            var type = objectEnum.GetType();
            return Enum.GetValues(type).Cast<int>().ToDictionary(e => e, e => Enum.GetName(type, e));
        }
        #endregion
    }
}
