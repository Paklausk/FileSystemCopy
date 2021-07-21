using System;
using FileSystemCopy.Views;

namespace FileSystemCopy.Presenters
{
    public abstract class Presenter<TView> : IPresenter<TView> where TView : class, IView
    {
        private readonly TView _view;
        public TView View
        {
            get { return _view; }
        }
        protected Presenter(TView view)
        {
            _view = view;
        }
        public abstract void Validate();
    }
}
