using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using Pri.LongPath;
using FileSystemCopy.Models;
using FileSystemCopy.Objects;
using FileSystemCopy.Presenters;

namespace FileSystemCopy
{
    //Supervising Controller
    public partial class MainWindow : Form
    {
        ProgressPresenter _progressPresenter;
        ProgressModel _progressModel = new ProgressModel();
        ProgressCalculator _progressCalculator = new ProgressCalculator();
        Thread _thread;
        bool _run = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            _progressPresenter = new ProgressPresenter(new ProgressBar(), _progressModel);
            _progressPresenter.Cancel += ProgressView_Cancel;
            _progressPresenter.Visible += ProgressView_Visible;
            folderBrowserDialog.SelectedPath = Paths.Instance.ExePath;
            Paths.Instance.OutputPath = Path.Combine(Paths.Instance.ExePath, Paths.Instance.DirectoryCloneName);
        }
        private void ProgressView_Visible(object sender, EventArgs e)
        {
            if (!_run)
            {
                StopThread();
                _progressCalculator.Reset();
                OnProgress(0, 1);
                StartThread();
            }
        }
        private void ProgressView_Cancel(object sender, EventArgs e)
        {
            OnCancel();
        }
        private void SelectDirectory_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                selectedPathBox.Text = folderBrowserDialog.SelectedPath;
        }
        private void Clone_Click(object sender, EventArgs e)
        {
            ShowProgressView();
        }
        private void RemoveAll_Click(object sender, EventArgs e)
        {
            Directory.Delete(Paths.Instance.OutputPath, true);
            MessageBox.Show("All clones removed");
        }
        private void Run()
        {
            List<ICreate> createList = new List<ICreate>();
            FileSystemReader fsReader = null;
            string[] directories = null;
            string[] files = null;
            int totalOperations = 0;
            if (_run)
                fsReader = new FileSystemReader(Paths.Instance.SelectedCloningPath);
            if (_run)
            {
                directories = fsReader.GetAllDirectories();
                _progressCalculator.Append(0.05f);
                OnProgress(0, 1);
            }
            if (_run)
            {
                files = fsReader.GetAllFiles();
                _progressCalculator.Append(0.05f);
                OnProgress(0, 1);
            }
            for (int i = 0; i < directories.Length; i++)
                if (_run)
                    createList.Add(new CreateFolder(Paths.Instance.OutputPath, PathConverter.Instance.Convert(directories[i], Paths.Instance.SelectedCloningPath)));
            if (_run)
            {
                _progressCalculator.Append(0.05f);
                OnProgress(0, 1);
            }
            for (int i = 0; i < files.Length; i++)
                if (_run)
                    createList.Add(new CreateFile(Paths.Instance.OutputPath, PathConverter.Instance.Convert(files[i], Paths.Instance.SelectedCloningPath)));
            if (_run)
            {
                _progressCalculator.Append(0.05f);
                OnProgress(0, 1);
            }
            totalOperations = createList.Count;
            for (int i = 0; i < createList.Count; i++)
                if (_run)
                {
                    createList[i].Create();
                    OnProgress(i + 1, totalOperations);
                }
            if (_run)
                OnFinish();
        }
        private void selectedPathBox_TextChanged(object sender, EventArgs e)
        {
            cloneButton.Enabled = Directory.Exists(selectedPathBox.Text);
            Paths.Instance.SelectedCloningPath = selectedPathBox.Text;
        }
        void OnProgress(int current, int total)
        {
            _progressModel.Progress = _progressCalculator.Calculate(current, total);
            _progressPresenter.Validate();
        }
        void OnCancel()
        {
            StopThread();
            CloseProgressView();
        }
        void OnFinish()
        {
            _run = false;
            CloseProgressView();
            BeginInvoke(new MethodInvoker(() => 
            {
                if (MessageBox.Show("Cloning finished") == DialogResult.OK)
                    if (Directory.Exists(Paths.Instance.OutputPath))
                        Process.Start(Paths.Instance.OutputPath);
            }));
        }
        void StartThread()
        {
            _run = true;
            _thread = new Thread(Run);
            _thread.Name = "Clone thread";
            _thread.Start();
        }
        void StopThread()
        {
            _run = false;
            if (_thread != null && !_thread.Join(500))
                _thread.Abort();
        }
        void ShowProgressView()
        {
            _progressPresenter.Show();
        }
        void CloseProgressView()
        {
            _progressPresenter.Close();
        }
    }
}
