using System;
using Pri.LongPath;

namespace FileSystemCopy.Objects
{
    public class CreateFile : ICreate
    {
        string _path;
        string _filePath;
        public CreateFile(string path, string filePath)
        {
            if (string.IsNullOrEmpty(path))
                throw new System.IO.IOException("Creation path cannot be null");
            if (string.IsNullOrEmpty(filePath))
                throw new System.IO.IOException("File path cannot be null");
            _path = path;
            _filePath = filePath;
        }
        public void Create()
        {
            string fullPath = Path.Combine(_path, _filePath);
            FileInfo fileInfo = new FileInfo(fullPath);
            if (!fileInfo.Exists)
            {
                System.IO.FileStream fileStream = fileInfo.Create();
                fileStream.Close();
                fileStream.Dispose();
            }
        }
    }
}
