//-----------------------------------------------------------------------
// <copyright file="ZipArchiveCompression.cs" company="StoryTeller.">
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
    using System;
    using System.IO;

    public class ZipArchiveCompression : ICompression
    {
        /// <summary>
        /// Compresses the file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        /// True if successful
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool CompressFile(string sourcePath, string destinationPath, out CompressedElement compressedFileInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Des the compress file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="deCompressed">The de compressed.</param>
        /// <returns>
        /// True if successful
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool DeCompressFile(string sourcePath, string destinationPath, out DeCompressedElement deCompressed)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compresses the folder.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        /// True if successful
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool CompressFolder(string sourcePath, string destinationPath, out CompressedElement compressedFileInfo)
        {
            throw new NotImplementedException();
        }
    }
}
