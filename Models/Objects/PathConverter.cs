using System;
using System.IO;

namespace FileSystemCopy.Objects
{
    public class PathConverter
    {
        static object _instanceMutex = new object();
        static PathConverter _instance;
        public static PathConverter Instance
        {
            get
            {
                lock (_instanceMutex)
                {
                    if (_instance == null)
                        _instance = new PathConverter();
                    return _instance;
                }
            }
        }
        private PathConverter()
        {
        }
        public string Convert(string fullPath, string oldPath)
        {
            DirectoryInfo di = new DirectoryInfo(oldPath);
            string pathExtension = fullPath.Replace(oldPath, string.Empty);
            pathExtension = Combine(di.Name, pathExtension);
            return pathExtension;
        }
        string Combine(string path1, string path2)
        {
            string path = string.Empty;
            if (!path1.EndsWith("\\") && !path2.StartsWith("\\"))
                path1 = path1 + "\\";
            path = path1 + path2;
            return path;
        }
    }
}
