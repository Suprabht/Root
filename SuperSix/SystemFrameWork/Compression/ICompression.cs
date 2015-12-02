

namespace SystemFrameWork.Compression
{
    using System.IO;
    /// <summary>
    /// Interface used for various compression
    /// </summary>
    internal interface ICompression
    {
        /// <summary>
        /// Compresses the file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <param name="name">The name.</param>
        /// <returns>True if successful</returns>
        bool CompressFile(string sourcePath, string destinationPath, out CompressedElement compressedFileInfo);

        /// <summary>
        /// Des the compress file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="deCompressed">The de compressed.</param>
        /// <param name="isFile">Indicate if the value is file</param>
        /// <returns>True if successful</returns>
        bool DeCompressFile(string sourcePath, string destinationPath, out DeCompressedElement deCompressed);

        /// <summary>
        /// Compresses the folder.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        bool CompressFolder(string sourcePath, string destinationPath, out CompressedElement compressedFileInfo);
        
    }
}
