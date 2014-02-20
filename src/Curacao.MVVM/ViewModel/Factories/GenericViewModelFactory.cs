using System;
using Curacao.MVVM.Services;
using JetBrains.Annotations;

namespace Curacao.MVVM.ViewModel.Factories
{
    public class GenericViewModelFactory<TViewModel> : IGenericViewModelFactory where TViewModel : BaseViewModel
    {
        protected readonly ISystemDispatcher Dispatcher;

        public GenericViewModelFactory([NotNull] ISystemDispatcher dispatcher)
        {
            if (dispatcher == null) throw new ArgumentNullException("dispatcher");
            Dispatcher = dispatcher;
        }

        public virtual BaseViewModel Create()
        {
            return (TViewModel) Activator.CreateInstance(typeof (TViewModel), new object[] { Dispatcher });
        }

        public virtual TViewModel CreateSpecific()
        {
            return (TViewModel) Activator.CreateInstance(typeof (TViewModel), new object[] {Dispatcher});
        }
    }
}