using System;

namespace MyStackOverflow.ViewModels.Services
{
    public interface ISystemDispatcher
    {
        void InvokeOnUIifNeeded(Action action);
    }
}