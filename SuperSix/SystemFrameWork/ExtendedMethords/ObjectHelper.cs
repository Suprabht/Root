namespace SystemFrameWork.ExtendedMethords
{
    using System;

    public static class ObjectHelper
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
        /// Gets the JSON string.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>JSON String</returns>
        /// <remarks>
        /// </remarks>
        public static string GetJsonString(this Object element)
        {
            return Json.JsonHelper.Serialize(element);
        }
        #endregion
    }
}
