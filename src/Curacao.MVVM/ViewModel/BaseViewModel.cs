using System;
using System.ComponentModel;
using Curacao.MVVM.Services;
using JetBrains.Annotations;

namespace Curacao.MVVM.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        [PublicAPI, NotNull] protected readonly ISystemDispatcher Dispatcher;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isLoading;

        protected BaseViewModel([NotNull] ISystemDispatcher dispatcher)
        {
            if (dispatcher == null) throw new ArgumentNullException("dispatcher");
            Dispatcher = dispatcher;
        }

        [NotifyPropertyChangedInvocator, PublicAPI]
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                Dispatcher.InvokeOnUIifNeeded(() => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access), PublicAPI]
        public bool IsLoading
        {
            get { return _isLoading; }
            protected set
            {
                if (value.Equals(_isLoading)) return;
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
    }
}