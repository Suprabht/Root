
namespace SystemFrameWork.Json
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public static class JsonHelper
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
        /// This method is use to convert a object to JSON string
        /// </summary>
        /// <param name="element"></param>
        /// <returns>JSON String</returns>
        public static string Serialize(object element)
        {
            //// Serialize the results as JSON
            return JsonConvert.SerializeObject(element);
        }


        /// <summary>
        /// Tries the deserialize to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">The json string.</param>
        /// <param name="newObject">The new object.</param>
        /// <returns>True if successfully converted.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static bool TryDeserializeToObject<T>(string jsonString, out T newObject)
        {
            bool success;
            
            try
            {
                newObject = JsonConvert.DeserializeObject<T>(jsonString);
                success = true;
            }
            catch (Exception)
            {
                newObject = default(T);
                throw new NotImplementedException();
            }
            
            return success;
        }


        /// <summary>
        /// Tries the deserialize to dictionary.
        /// </summary>
        /// <param name="jsonString">The json string.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>True if successfully converted.</returns>
        public static bool TryDeserializeToDictionary(string jsonString, out IDictionary<string, object> dictionary)
        {
            return TryDeserializeToObject(jsonString, out dictionary);
        }

        #endregion
    }
}
