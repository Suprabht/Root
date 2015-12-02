
namespace SystemFrameWork.Compression
{
    using System.IO;

    public class DeCompressedElement
    {
        #region Private Variable

        #endregion

        #region Properties

        /// <summary>
        /// Gets the new compressed file.
        /// </summary>
        /// <value>
        /// The new compressed file.
        /// </value>
        public object NewCompressedObject { get; }

        /// <summary>
        /// Gets from object.
        /// </summary>
        /// <value>
        /// From object.
        /// </value>
        public FileInfo FromFile { get; }

        /// <summary>
        /// Gets a value indicating whether [was file].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [was file]; otherwise, <c>false</c>.
        /// </value>
        public bool IsFile { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DeCompressedElement"/> class.
        /// </summary>
        /// <param name="newDeCompressedObject">The new compressed object.</param>
        /// <param name="fromFile">From file.</param>
        /// <param name="isFile">if set to <c>true</c> [is file].</param>
        public DeCompressedElement(object newDeCompressedObject, FileInfo fromFile, bool isFile)
        {
            NewCompressedObject = newDeCompressedObject;
            FromFile = fromFile;
            IsFile = isFile;
        }

        #endregion

        #region private methods

        #endregion

        #region Public methods

        #endregion

    }
}
