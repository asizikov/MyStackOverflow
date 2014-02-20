using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Curacao.MVVM.Navigation;
using Curacao.MVVM.Services;
using Curacao.MVVM.ViewModel;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.Model;
using MyStackOverflow.Model.Internal;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly INavigationService _navigation;
        private readonly IApplicationSettings _settings;
        private readonly StatisticsService _statistics;
        private string _query;
        private IDisposable _queryObserver;
        private ObservableCollection<SearchResultItem> _searchResult;
        private SearchResultItem _selectedProfile;

        public LoginViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] INavigationService navigation,
            [NotNull] IApplicationSettings settings, [NotNull] AsyncDataProvider dataProvider,
            [NotNull] StatisticsService statistics)
            : base(dispatcher)
        {
            if (navigation == null) throw new ArgumentNullException("navigation");
            if (settings == null) throw new ArgumentNullException("settings");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (statistics == null) throw new ArgumentNullException("statistics");
            _navigation = navigation;
            _settings = settings;
            _dataProvider = dataProvider;
            _statistics = statistics;
            SubscribeToQuery();
            _statistics.PublishLoginPageLoaded();
            SelectedProfile = null;
            SearchResult = new ObservableCollection<SearchResultItem>();
        }

        [UsedImplicitly(ImplicitUseKindFlags.Default)]
        public string Query
        {
            get { return _query; }
            set
            {
                if (value == _query) return;
                _query = value;
                OnPropertyChanged("Query");
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access),CanBeNull]
        public ObservableCollection<SearchResultItem> SearchResult
        {
            get
            {
                return _searchResult;
            }
            private set
            {
                if (Equals(value, _searchResult)) return;
                _searchResult = value;
                OnPropertyChanged("SearchResult");
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access), CanBeNull]
        public SearchResultItem SelectedProfile
        {
            get
            {
                return _selectedProfile;
            }
            set
            {
                if (Equals(value, _selectedProfile)) return;
                _selectedProfile = value;
                OnPropertyChanged("SelectedProfile");
                if (_selectedProfile != null)
                {
                    GoToProfile(_selectedProfile.Id);
                }
            }
        }

        private void SubscribeToQuery()
        {
            _queryObserver = (from evt in Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged")
                where evt.EventArgs.PropertyName == "Query"
                select Query)
                .Throttle(TimeSpan.FromMilliseconds(1000))
                .DistinctUntilChanged()
                .Subscribe(GetResults);
        }

        private void GetResults(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                SearchResult = new ObservableCollection<SearchResultItem>();
            }
            else
            {
                IsLoading = true;
                _dataProvider.GetUsersByString(query)
                    .Subscribe(HandleResults, ex => { IsLoading = false; });
            }
        }

        private void HandleResults(UsersResponce result)
        {
            if (result != null && result.Users != null)
            {
                SearchResult =
                    new ObservableCollection<SearchResultItem>(result.Users.Select(u => new SearchResultItem(u)));
            }
            else
            {
                SearchResult = new ObservableCollection<SearchResultItem>();
            }
        }

        private void GoToProfile(int id)
        {
            _settings.Settings.Me = new Me
            {
                Id = id.ToString()
            };
            _navigation.GoToPage(Pages.ProfilePage, new[]
            {
                new NavigationParameter
                {
                    Parameter = NavigationParameterName.Id,
                    Value = id.ToString()
                }
            }, 1);
        }
    }
}