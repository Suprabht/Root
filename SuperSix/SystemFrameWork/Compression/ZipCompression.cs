

namespace SystemFrameWork.Compression
{
    using System;
    using System.IO;

    public class ZipCompression:ICompression
    {
        /// <summary>
        /// Compresses the file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <returns>True if successful</returns>
        public bool CompressFile(string sourcePath, string destinationPath, out CompressedElement compressedFileInfo)
        {
            bool result = false;
            compressedFileInfo = null;
            try
            {
                //using (ZipFile zipFile = ZipFile.Create(destinationPath))
                //{
                //    zipFile.BeginUpdate();

                //    add the file to the zip file
                //    zipFile.Add(sourcePath);

                //    commit the update once we are done
                //    zipFile.CommitUpdate();
                //    close the file
                //    zipFile.Close();
                //}

                result = true;
                compressedFileInfo = new CompressedElement(new FileInfo(destinationPath), new FileInfo(sourcePath), true);
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
        public bool DeCompressFile(string sourcePath, string destinationPath, out DeCompressedElement deCompressed)
        {
            bool result = false;
            try
            {
               // System.IO.Compression.ZipFile.ExtractToDirectory(sourcePath, destinationPath);               
                deCompressed = new DeCompressedElement(new object(), new FileInfo(sourcePath), false);
            }
            catch(Exception)
            {
                deCompressed = null;
            }
            return result;
        }

        /// <summary>
        /// Compresses the folder.
        /// </summary>
        /// <param name="sourcePath">The source path eg: @"c:\example\start" .</param>
        /// <param name="destinationPath">The destination path eg: @"c:\example\result.zip".</param>
        /// <param name="compressedFileInfo">The compressed file information.</param>
        /// <returns>True if successful</returns>
        public bool CompressFolder(string sourcePath, string destinationPath, out CompressedElement compressedFileInfo)
        {
            bool result = false;
           
            try
            {
                                      
               // System.IO.Compression.ZipFile.CreateFromDirectory(sourcePath, destinationPath);
                compressedFileInfo = new CompressedElement(new FileInfo(destinationPath), new DirectoryInfo(sourcePath), false);
                result = true;
            }
            catch(Exception)
            {
                result = false;
                compressedFileInfo = null;
            }
            
            return result;
        }
    }
}
