using System;
using System.Windows.Forms;
using FileSystemCopy.Views;
using FileSystemCopy.Models.Objects;

namespace FileSystemCopy
{
    //Passive View
    public partial class ProgressBar : Form, IProgressView
    {
        public event EventHandler CancelClicked;
        public new event VisibleEventHandler VisibleChanged;
        public ProgressBar()
        {
            InitializeComponent();
            base.VisibleChanged += new EventHandler(ProgressBar_VisibleChanged);
        }
        public float Progress
        {
            get { return progressBar1.Value / (float)progressBar1.Maximum; }
            set
            {
                progressBar1.Value = (int)(progressBar1.Maximum * value);
            }
        }
        public new void Invoke(Delegate method)
        {
            BeginInvoke(method);
        }
        public new void Invoke(Delegate method, params object[] args)
        {
            BeginInvoke(method, args);
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            if (CancelClicked != null)
                CancelClicked(this, null);
        }
        private void ProgressBar_VisibleChanged(object sender, EventArgs e)
        {
            if (VisibleChanged != null)
                VisibleChanged(this, new VisibleEventArgs(Visible));
        }
    }
}
