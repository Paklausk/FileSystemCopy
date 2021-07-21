using System;
using System.IO;

namespace FileSystemCopy.Objects
{
    public class FileSystemReader
    {
        DirectoryInfo _di;
        public FileSystemReader(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new IOException("Path cannot be empty");
            _di = new DirectoryInfo(path);
        }
        public string[] GetAllDirectories()
        {
            DirectoryInfo[] dis = _di.GetDirectories("*", SearchOption.AllDirectories);
            string[] directories = new string[dis.Length];
            for (int i = 0; i < dis.Length; i++ )
                directories[i] = dis[i].FullName;
            return directories;
        }
        public string[] GetAllFiles()
        {
            FileInfo[] fileInfos = _di.GetFiles("*", SearchOption.AllDirectories);
            string[] files = new string[fileInfos.Length];
            for (int i = 0; i < fileInfos.Length; i++)
                files[i] = fileInfos[i].FullName;
            return files;
        }
    }
}
