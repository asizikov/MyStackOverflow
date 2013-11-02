using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace MyStackOverflow.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly ISystemDispatcher _dispatcher;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isLoading;

        protected BaseViewModel([NotNull] ISystemDispatcher dispatcher)
        {
            if (dispatcher == null) throw new ArgumentNullException("dispatcher");
            _dispatcher = dispatcher;
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                _dispatcher.BeginInvokeOnUIifNeeded(() => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
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