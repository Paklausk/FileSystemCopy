using System;
using System.Windows.Forms;

namespace FileSystemCopy.Views
{
    public interface IView
    {
        void Invoke(Delegate method);
        void Invoke(Delegate method, params object[] args);
        DialogResult ShowDialog();
        void Close();
    }
    public interface IView<TModel> : IView
    {
        TModel Model { get; set; }
    }
}
