using System;
using FileSystemCopy.Views;

namespace FileSystemCopy.Presenters
{
    public interface IPresenter
    {
        void Validate();
    }
    public interface IPresenter<TView> : IPresenter where TView : class, IView
    {
        TView View { get; }
    }
}
