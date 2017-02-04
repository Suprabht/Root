
namespace SystemFrameWork.ExtendedMethords
{
    using System.IO;
    using Compression;

    public static class FileInfoHelper
    {
        /// <summary>
        /// Compresses the specified type.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="type">The type.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <returns>True if successful</returns>
        public static bool Compress(this FileInfo fileInfo, CompressionType type, string destinationPath,
            out CompressedElement compressedFileInfo)
        {
            bool success;
            ICompression compression;
            switch (type)
            {
                case CompressionType.GZip:
                    compression = new GZipCompression();
                    success = compression.CompressFolder(fileInfo.FullName, destinationPath, out compressedFileInfo);
                    break;
                case CompressionType.ZipArchive:
                    compression = new ZipArchiveCompression();
                    success = compression.CompressFolder(fileInfo.FullName, destinationPath, out compressedFileInfo);
                    break;
                default:
                    compression = new ZipCompression();
                    success = compression.CompressFolder(fileInfo.FullName, destinationPath, out compressedFileInfo);
                    break;

            }
            return success;
        }

        ///// <summary>
        ///// De-compress the file.
        ///// </summary>
        ///// <param name="fileInfo">The file information.</param>
        ///// <param name="destinationPath">The destination path.</param>
        ///// <param name="deCompressed">The de compressed.</param>
        ///// <returns>True if successful</returns>
        public static bool DeCompressFile(this FileInfo fileInfo, string destinationPath, out DeCompressedElement deCompressed)
        {
            bool success;
            ICompression compression;
            switch (fileInfo.Extension.ToLower())
            {
                case "gz":
                    compression = new GZipCompression();
                    success = compression.DeCompressFile(fileInfo.FullName, destinationPath, out deCompressed);
                    break;
                case "zip":
                    compression = new ZipCompression();
                    success = compression.DeCompressFile(fileInfo.FullName, destinationPath, out deCompressed);
                    break;
                case "ziparc":
                    compression = new ZipArchiveCompression();
                    success = compression.DeCompressFile(fileInfo.FullName, destinationPath, out deCompressed);
                    break;
                default:
                    success = false;
                    deCompressed = null;
                    break;

            }
            return success;
        }
    }
}
