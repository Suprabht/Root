

namespace SystemFrameWork.Compression
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public class GZipCompression:ICompression
    {
        /// <summary>
        /// Compresses the file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <returns>
        /// True if successful
        /// </returns>
        public bool CompressFile(string sourcePath, string destinationPath, out CompressedElement compressedFileInfo)
        {
            bool result;
            compressedFileInfo = null;
            try
            {
                FileInfo fileToCompress = new FileInfo(sourcePath);
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) &
                       FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                               CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);

                            }
                        }
                        compressedFileInfo = new CompressedElement(new FileInfo(destinationPath + "\\" + fileToCompress.Name + ".gz"), fileToCompress, true);
                    }

                }
                result = true;
            }
            catch(Exception)
            {
                result = false;
                compressedFileInfo = null;
            }

            return result;
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
            bool result = false;
            deCompressed = null;
            try
            {
                FileInfo fileToDecompress = new FileInfo(sourcePath);
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                    using (FileStream decompressedFileStream = File.Create(newFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                            /*TODO:Implement the deCompressed object logic*/
                            // Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                        }
                    }
                }
            }
            catch(Exception)
            {
                result = false;
            }            

            return result;
        }

        /// <summary>
        /// Compresses the folder.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <returns>
        /// True if successful
        /// </returns>
        public bool CompressFolder(string sourcePath, string destinationPath, out CompressedElement compressedFileInfo)
        {
            bool result;
            compressedFileInfo = null;
            try
            {
                DirectoryInfo directorySelected = new DirectoryInfo(sourcePath);
                foreach (FileInfo fileToCompress in directorySelected.GetFiles())
                {
                    using (FileStream originalFileStream = fileToCompress.OpenRead())
                    {
                        if ((File.GetAttributes(fileToCompress.FullName) &
                           FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                        {
                            using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                            {
                                using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                                   CompressionMode.Compress))
                                {
                                    originalFileStream.CopyTo(compressionStream);

                                }
                            }
                            compressedFileInfo = new CompressedElement(new FileInfo(destinationPath + "\\" + fileToCompress.Name + ".gz"), directorySelected, false);
                        }

                    }
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
                compressedFileInfo = null;
            }
            return result;
        }
    }
}
