using System;
using System.Windows.Forms;
using FileSystemCopy.Views;
using FileSystemCopy.Models;
using FileSystemCopy.Models.Objects;

namespace FileSystemCopy.Presenters
{
    public class ProgressPresenter : Presenter<IProgressView>
    {
        ProgressModel _model;
        public event EventHandler Cancel;
        public event EventHandler Visible;
        public event EventHandler Invisible;
        public ProgressPresenter(IProgressView view, ProgressModel model) : base(view)
        {
            _model = model;
            View.CancelClicked += View_CancelClicked;
            View.VisibleChanged += View_VisibleChanged;
        }
        public override void Validate()
        {
            SetProgress(_model.Progress);
        }
        public void Show()
        {
            View.ShowDialog();
        }
        public void Close()
        {
            View.Invoke(new MethodInvoker(() =>
               {
                   View.Close();
               }));
        }
        void SetProgress(float value)
        {
            if (value < 0f || value > 1f)
                throw new Exception("Operation is invalid");
            View.Invoke(new MethodInvoker(() =>
            {
                View.Progress = value;
                //Log.Instance.Debug(string.Format("Progress - {0:0.00}", value));
            }));
        }
        void View_CancelClicked(object sender, EventArgs e)
        {
            if (Cancel != null)
                Cancel(this, EventArgs.Empty);
        }
        void View_VisibleChanged(object sender, VisibleEventArgs e)
        {
            if (e.IsVisible)
            {
                if (Visible != null)
                    Visible(this, EventArgs.Empty);
            }
            else
            {
                if (Invisible != null)
                    Invisible(this, EventArgs.Empty);
            }
        }
    }
}
