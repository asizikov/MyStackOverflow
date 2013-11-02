using System;

namespace MyStackOverflow.ViewModels
{
    public interface ISystemDispatcher
    {
        void BeginInvokeOnUIifNeeded(Action action);
    }
}