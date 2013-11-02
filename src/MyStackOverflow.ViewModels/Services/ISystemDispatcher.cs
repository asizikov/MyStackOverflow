using System;

namespace MyStackOverflow.ViewModels
{
    public interface ISystemDispatcher
    {
        void InvokeOnUIifNeeded(Action action);
    }
}