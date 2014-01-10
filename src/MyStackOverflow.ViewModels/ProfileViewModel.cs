using System;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.Model;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly int _id;
        private readonly StatisticsService _statistics;
        private readonly IStringsProvider _stringsProvider;
        private ObservableCollection<Badge> _badges;
        private UserViewModel _user;

        public ProfileViewModel(ISystemDispatcher dispatcher, [NotNull] AsyncDataProvider dataProvider, int id,
            [NotNull] StatisticsService statistics, [NotNull] IStringsProvider stringsProvider)
            : base(dispatcher)
        {
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            _dataProvider = dataProvider;
            _id = id;
            _statistics = statistics;
            _stringsProvider = stringsProvider;
            Initialize();
            _statistics.ReportProfilePageLoaded();
        }

        [CanBeNull, UsedImplicitly(ImplicitUseKindFlags.Access)]
        public UserViewModel User
        {
            get { return _user; }
            private set
            {
                if (Equals(value, _user)) return;
                _user = value;
                OnPropertyChanged("User");
            }
        }

        [CanBeNull, UsedImplicitly(ImplicitUseKindFlags.Access)]
        public ObservableCollection<Badge> Badges
        {
            get { return _badges; }
            private set
            {
                if (Equals(value, _badges)) return;
                _badges = value;
                OnPropertyChanged("Badges");
            }
        }

        private void Initialize()
        {
            IsLoading = true;
            _dataProvider.GetUsersAsync(_id)
                .Subscribe(result =>
                {
                    if (result != null && result.Total > 0)
                    {
                        User user = result.Users.First();
                        User = new UserViewModel(Dispatcher, user, _stringsProvider);
                        LoadBagesInfo(_id);
                    }
                }, ex => { IsLoading = false; });
        }

        private void LoadBagesInfo(int id)
        {
            _dataProvider.GetUserBagesList(id)
                .Subscribe(bages =>
                {
                    if (bages != null)
                    {
                        Badges = new ObservableCollection<Badge>(bages.Badges);
                    }
                    IsLoading = false;
                }, ex => { IsLoading = false; }
                );
        }
    }
}