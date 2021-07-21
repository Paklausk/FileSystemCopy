using System;
using FileSystemCopy.Models.Objects;

namespace FileSystemCopy.Views
{
    public interface IProgressView : IView
    {
        event EventHandler CancelClicked;
        event VisibleEventHandler VisibleChanged;
        float Progress { get; set; }
    }
}
