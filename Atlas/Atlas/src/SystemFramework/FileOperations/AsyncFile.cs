
namespace SystemFrameWork.FileOperations
{
    using System;
    using System.IO;

    /// <summary>
    /// This class is responsible for the file operations Asynchronously.
    /// </summary>
    public class AsyncFile
    {
        #region Private Variable

        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Gets the FilePath of the file this include the name as well as the extension of the file.
        /// </summary>
        public String FilePath { get; private set; }

        /// <summary>
        /// Gets the CreationTime of the file.
        /// </summary>
        public DateTime? CreationTime { get; private set; }

        /// <summary>
        /// Gets the instance of parent directory of the file.
        /// </summary>
        public DirectoryInfo Directory { get; private set; }

        /// <summary>
        /// Gets a value indicating whether a file exists.
        /// </summary>
        public bool Exists { get; private set; }

        /// <summary>
        /// Gets the string representing the extension part of the file.
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// Gets or sets a value that determines if the current file is read only.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.
        /// </summary>
        public DateTime? LastAccessTime { get; private set; }

        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        public DateTime? LastWriteTime { get; private set; }

        /// <summary>
        /// Gets the size, in bytes, of the current file.
        /// </summary>
        public long Length { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to create the AsyncFile object
        /// </summary>
        /// <param name="filePath"></param>
        public AsyncFile(String filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                InitializeFile(file);
            }
            else
            {
                ResetFile();
                FilePath = filePath;
            }
        }

        /// <summary>
        /// Copy Constructor to copy file
        /// </summary>
        /// <param name="oldfile"></param>
        public AsyncFile(AsyncFile oldfile)
        {
            FileInfo file = new FileInfo(oldfile.FilePath);
            if (file.Exists)
            {
                InitializeFile(file);
            }
            else
            {
                throw new Exception();
            }

        }
        #endregion

        #region private methods

        /// <summary>
        /// This is a helper function to initially properties of the AsyncFile class use mostly in constructor 
        /// </summary>
        /// <param name="fileInfo"></param>
        private void InitializeFile(FileInfo fileInfo)
        {
            Name = fileInfo.Name;
            FilePath = fileInfo.FullName;
            CreationTime = fileInfo.CreationTime;
            Directory = fileInfo.Directory;
            Exists = fileInfo.Exists;
            Extension = fileInfo.Extension;
            IsReadOnly = fileInfo.IsReadOnly;
            LastAccessTime = fileInfo.LastAccessTimeUtc;
            LastWriteTime = fileInfo.LastWriteTime;
            Length = fileInfo.Length;
        }

        /// <summary>
        /// Private function to Rest file to default
        /// </summary>
        private void ResetFile()
        {
            CreationTime = null;
            Directory = null;
            Exists = false;
            Extension = string.Empty;
            IsReadOnly = false;
            LastAccessTime = null;
            LastWriteTime = null;
            Length = 0;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Append text to the file
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isAsync"></param>
        /// <returns>The entire string</returns>
        public string AppendText(string text, bool isAsync)
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            string addedText = string.Empty;
            try
            {
                StreamWriter streamWriter = fileInfo.AppendText();
                streamWriter.WriteLine(text);
                streamWriter.Flush();
                streamWriter.Dispose();
                StreamReader streamReader = new StreamReader(fileInfo.OpenRead());
                while (streamReader.Peek() != -1)
                    addedText += streamReader.ReadLine();
            }
            catch (FileNotFoundException)
            {
                throw new NotImplementedException();
            }
            catch (UnauthorizedAccessException)
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

            return addedText;
        }

        /// <summary>
        /// To copy a file to definite location 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isOverWrite"></param>
        /// <param name="isAsync"></param>
        /// <returns>The new AsyncFile Object</returns>
        public AsyncFile CopyTo(string path, bool isOverWrite, bool isAsync)
        {
            var fileInfo = new FileInfo(FilePath);
            try
            {
                fileInfo.CopyTo(path);
            }
            catch (ArgumentException)
            {
                throw new NotImplementedException();
            }
            catch (IOException)
            {
                throw new NotImplementedException();
            }
            catch (UnauthorizedAccessException)
            {
                throw new NotImplementedException();
            }
            catch (NotSupportedException)
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

            return new AsyncFile(path);
        }

        /// <summary>
        /// Create a file at filePath
        /// </summary>
        /// <returns>Return the FileStream of the Created file</returns>
        public FileStream Create()
        {
            FileInfo file = new FileInfo(FilePath);
            FileStream fileStream;
            try
            {
                fileStream = file.Create();
                InitializeFile(file);
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

            return fileStream;
        }

        /// <summary>
        /// Creates a new text file.
        /// </summary>
        /// <param name="isAsync"></param>
        /// <param name="text"></param>
        public void CreateText(bool isAsync, string text = "")
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            try
            {
                StreamWriter streamWriter = fileInfo.CreateText();
                if (!string.IsNullOrEmpty(text))
                {
                    streamWriter.WriteLine(text);
                }
                streamWriter.Dispose();
                InitializeFile(fileInfo);
            }
            catch (UnauthorizedAccessException)
            {
                throw new NotImplementedException();
            }
            catch (IOException)
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        /// <summary>
        /// Delete the files
        /// </summary>
        public void Delete()
        {
            try
            {
                (new FileInfo(FilePath)).Delete();
                ResetFile();
            }
            catch (UnauthorizedAccessException)
            {
                throw new NotImplementedException();
            }
            catch (IOException)
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Moves a specified file to a new location, providing the option to specify a new file name.
        /// </summary>
        /// <param name="newFileName"></param>
        /// <param name="isAsync"></param>
        /// <returns></returns>
        public AsyncFile MoveTo(string newFileName, bool isAsync)
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            try
            {
                fileInfo.MoveTo(newFileName);
                ResetFile();
            }
            catch (FileNotFoundException)
            {
                throw new NotImplementedException();
            }

            return (new AsyncFile(newFileName));
        }

        /// <summary>
        /// Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.
        /// </summary>
        /// <param name="fileMode"></param>
        /// <param name="fileAccess"></param>
        /// <param name="fileShare"></param>
        /// <returns>FileStream</returns>
        public FileStream Open(FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            FileStream fileStream;
            try
            {
                fileStream = fileInfo.Open(fileMode, fileAccess, fileShare);
            }
            catch (FileNotFoundException)
            {
                throw new NotImplementedException();
            }
            catch (UnauthorizedAccessException)
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

            return fileStream;
        }

        /// <summary>
        /// Creates a read-only FileStream.
        /// </summary>
        /// <returns>FileStream</returns>
        public FileStream OpenRead()
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            FileStream fileStream;
            try
            {
                fileStream = fileInfo.OpenRead();
            }
            catch (IOException)
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

            return fileStream;
        }

        /// <summary>
        /// Creates a StreamReader with UTF8 encoding that reads from an existing text file.
        /// </summary>
        /// <returns>StreamReader</returns>
        public StreamReader OpenText()
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            StreamReader streamReader;
            try
            {
                streamReader = fileInfo.OpenText();
            }
            catch (IOException)
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

            return streamReader;
        }

        /// <summary>
        /// Creates a write-only FileStream.
        /// </summary>
        /// <returns>FileStream</returns>
        public FileStream OpenWrite(bool isAsync)
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            FileStream fileStream;
            try
            {
                fileStream = fileInfo.OpenWrite();
            }
            catch (UnauthorizedAccessException)
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

            return fileStream;
        }

        #endregion
    }
}
