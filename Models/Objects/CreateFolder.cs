using System;
using Pri.LongPath;

namespace FileSystemCopy.Objects
{
    public class CreateFolder : ICreate
    {
        string _path;
        string _folderPath;
        public CreateFolder(string path, string folderPath)
        {
            if (string.IsNullOrEmpty(path))
                throw new System.IO.IOException("Creation path cannot be empty");
            if (string.IsNullOrEmpty(folderPath))
                throw new System.IO.IOException("Folder path cannot be empty");
            _path = path;
            _folderPath = folderPath;
        }
        public void Create()
        {
            string fullPath = Path.Combine(_path, _folderPath);
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
            if (!directoryInfo.Exists)
                directoryInfo.Create();
        }
    }
}
