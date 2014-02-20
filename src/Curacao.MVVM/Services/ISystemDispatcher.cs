using System;

namespace Curacao.MVVM.Services
{
    public interface ISystemDispatcher
    {
        void InvokeOnUIifNeeded(Action action);
    }
}