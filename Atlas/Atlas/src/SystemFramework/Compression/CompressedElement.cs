//-----------------------------------------------------------------------
// <copyright file="CompressedElement.cs" company="StoryTeller.">
//     Copyright (c) StoryTeller 2016. All rights reserved.
// </copyright>
// <author>Suprabhat Paul</author>
// <createddate>6/15/2016 7:22:55 PM</createddate>
// <changelog>
//  username        date        reason
//  --------        ----        ------
//                 
// </changelog>
//-----------------------------------------------------------------------

namespace SystemFrameWork.Compression
{
    using System.IO;

    /// <summary>
    /// This class is responsible for the file operations Asynchronously.
    /// </summary>
    public class CompressedElement
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
        public FileInfo NewCompressedFile { get; }

        /// <summary>
        /// Gets from object.
        /// </summary>
        /// <value>
        /// From object.
        /// </value>
        public object FromObject { get; }

        /// <summary>
        /// Gets a value indicating whether [was file].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [was file]; otherwise, <c>false</c>.
        /// </value>
        public bool WasFile { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CompressedElement"/> class.
        /// </summary>
        /// <param name="newCompressedFile">The new compressed file.</param>
        public CompressedElement(FileInfo newCompressedFile, object fromObject, bool wasFile)
        {
            NewCompressedFile = newCompressedFile;
            FromObject = fromObject;
            WasFile = wasFile;
        }

        #endregion

        #region private methods

        #endregion

        #region Public methods

        #endregion


        
    }
}
