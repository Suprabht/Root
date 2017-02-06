

namespace SystemFrameWork.Xml
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    // using System.Xml;
    //  using System.Xml.Linq;

    public static class XmlHelper
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
        /// Converts to typed object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString">The XML string.</param>
        /// <returns></returns>
        public static T ConvertToTypedObject<T>(this string xmlString) where T : class
        {
            var reader = XmlReader.Create(new MemoryStream(Encoding.UTF8.GetBytes(xmlString.Trim())), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

        /// <summary>
        /// Converts to XML string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typedObject">The typed object.</param>
        /// <returns></returns>
        public static string ConvertToXmlString<T>(object typedObject)
        {
            XmlSerializer serialize = new XmlSerializer(typeof(T));
            StringWriter outStream = new StringWriter();
            serialize.Serialize(outStream, typedObject);
            return outStream.ToString();
        }

        
        #endregion
    }
}
