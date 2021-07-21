using System;

namespace FileSystemCopy.Models.Objects
{
    public class VisibleEventArgs : EventArgs
    {
        public bool IsVisible { get; set; }
        public VisibleEventArgs()
        {
            IsVisible = false;
        }
        public VisibleEventArgs(bool visible)
        {
            IsVisible = visible;
        }
    }
}
