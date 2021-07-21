using System;
using System.IO;
using System.Windows.Forms;

namespace FileSystemCopy.Objects
{
    public class Paths
    {
        static object _instanceMutex = new object();
        static Paths _instance;
        public static Paths Instance
        {
            get
            {
                lock (_instanceMutex)
                {
                    if (_instance == null)
                        _instance = new Paths();
                    return _instance;
                }
            }
        }
        private Paths()
        {
        }

        string _exePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
        string _directoryCloneName = "Clone";
        string _selectedCloningPath = null;
        string _outputPath = null;

        public string ExePath
        {
            get { return _exePath; }
        }
        public string DirectoryCloneName
        {
            get { return _directoryCloneName; }
        }
        public string SelectedCloningPath
        {
            get
            {
                if (string.IsNullOrEmpty(_selectedCloningPath))
                    throw new Exception("Cloning path wasn't selected");
                return _selectedCloningPath;
            }
            set { _selectedCloningPath = value; }
        }
        public string OutputPath
        {
            get
            {
                if (string.IsNullOrEmpty(_outputPath))
                    throw new Exception("Output path wasn't set");
                return _outputPath;
            }
            set { _outputPath = value; }
        }
    }
}
