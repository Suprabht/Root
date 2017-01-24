

namespace SystemFrameWork.ExtendedMethords
{
    using System.IO;
    using Compression;

    public static class DirectoryInfoHelper
    {

        /// <summary>
        /// Compresses the specified type.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="type">The type.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <param name="name">The name.</param>
        /// <returns>True if succeeded</returns>
        public static bool Compress(this DirectoryInfo directory, CompressionType type, string destinationPath, out CompressedElement compressedFileInfo, string name = "")
        {
            bool success;
            ICompression compression;
            switch (type)
            {
                case CompressionType.GZip:
                    compression = new GZipCompression();
                    success = compression.CompressFolder(directory.FullName, destinationPath, out compressedFileInfo);
                    break;
                case CompressionType.ZipArchive:
                    compression = new ZipArchiveCompression();
                    success = compression.CompressFolder(directory.FullName, destinationPath, out compressedFileInfo);
                    break;
                default:
                    compression = new ZipCompression();
                    success = compression.CompressFolder(directory.FullName, destinationPath, out compressedFileInfo);
                    break;

            }
            return success;

        }
    }
}
