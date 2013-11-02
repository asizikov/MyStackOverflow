using System;
using System.Linq;
using JetBrains.Annotations;
using MyStackOverflow.Data;

namespace MyStackOverflow.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private string _reputation;

        public MainViewModel(ISystemDispatcher dispatcher, [NotNull] AsyncDataProvider dataProvider)
            : base(dispatcher)
        {
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            _dataProvider = dataProvider;
            Initialize();
        }

        private void Initialize()
        {
            IsLoading = true;
            _dataProvider.GetUsersAsync(555014)
                .Subscribe(result =>
                {
                    if (result != null && result.Total > 0)
                    {
                        Reputation = result.Users.First().reputation.ToString();
                    }
                }, ex => { IsLoading = false; });
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        public string Reputation
        {
            get { return _reputation; }
            private set
            {
                if (value == _reputation) return;
                _reputation = value;
                OnPropertyChanged("Reputation");
            }
        }
    }
}