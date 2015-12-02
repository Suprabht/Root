using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

namespace SystemFrameWork.ExtendedMethords
{
    /// <summary>
    /// This class comprises of some commonly use string function
    /// </summary>
    public static class StringHelper
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
        /// Method is used to split a string in to an array of string which take a string as split parameter
        /// </summary>
        /// <param name="content"></param>
        /// <param name="seperator"></param>
        /// <returns>Array of string</returns>
        public static string[] Split(this String content, string seperator)
        {
            return Regex.Split(content, seperator);
        }


        /// <summary>
        /// Tries to convert Json String to dictionary.
        /// </summary>
        /// <param name="jsonString">The json string.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>boolean value true if converted successfully.</returns>
        public static bool TryConvertToDictionary(this String jsonString, out IDictionary<string, object> dictionary )
        {
            
            return Json.JsonHelper.TryDeserializeToDictionary(jsonString, out dictionary);
        }

        /// <summary>
        /// De-serialize a JSON string to typed object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">The json string.</param>
        /// <param name="typedObject">Typed object</param>
        /// <returns>boolean value true if converted successfully.</returns>
        /// <remarks>
        ///    How to use?
        ///    string jsonString = "{\"Age\":28,\"Name\":\"Tom\"}";
        ///    Person p = jsonString.ConvertToType &lt;person&gt;();
        /// </remarks>
        public static bool TryConvertToType<T>(this String jsonString, out T typedObject)
        {
            
            return Json.JsonHelper.TryDeserializeToObject(jsonString, out typedObject);
        }


        /// <summary>
        /// Generates the stream from string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static Stream GenerateStreamFromString(this String text)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        /// <summary>
        /// To the boolean.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBoolean(this String value)
        {
            bool result;
            bool pass = bool.TryParse(value, out result);
            return pass && result;
        }
        #endregion
    }
}
