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
        private const string URL_GRAVATAR = "http://www.gravatar.com/avatar/{0}?s=128&d=identicon&r=PG";

        private readonly AsyncDataProvider _dataProvider;
        private readonly int _id;
        private readonly StatisticsService _statistics;
        private string _reputation;
        private string _displayName;
        private string _location;
        private int _bronzeBages;
        private int _silverBages;
        private int _goldBages;
        private string _userPic;
        private ObservableCollection<Badge> _badges;

        public ProfileViewModel(ISystemDispatcher dispatcher, [NotNull] AsyncDataProvider dataProvider, int id,
            [NotNull] StatisticsService statistics)
            : base(dispatcher)
        {
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (statistics == null) throw new ArgumentNullException("statistics");
            _dataProvider = dataProvider;
            _id = id;
            _statistics = statistics;
            Initialize();
            _statistics.ReportProfilePageLoaded();
        }

        private void Initialize()
        {
            IsLoading = true;
            _dataProvider.GetUsersAsync(_id)
                .Subscribe(result =>
                {
                    if (result != null && result.Total > 0)
                    {
                        var user = result.Users.First();
                        Reputation = user.Reputation.ToString();
                        DisplayName = user.DisplayName;
                        Location = user.Location;
                        GoldBages = user.BadgeCounts.Gold;
                        SilverBages = user.BadgeCounts.Silver;
                        BronzeBages = user.BadgeCounts.Bronze;
                        UserPic = user.EmailHash;
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

        public string UserPic
        {
            get
            {
                return string.IsNullOrWhiteSpace(_userPic)
                    ? string.Empty
                    : string.Format(URL_GRAVATAR, _userPic);
            }
            private set
            {
                if (value == _userPic) return;
                _userPic = value;
                OnPropertyChanged("UserPic");
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

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        public int BronzeBages
        {
            get { return _bronzeBages; }
            private set
            {
                if (value == _bronzeBages) return;
                _bronzeBages = value;
                OnPropertyChanged("BronzeBages");
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        public int SilverBages
        {
            get { return _silverBages; }
            private set
            {
                if (value == _silverBages) return;
                _silverBages = value;
                OnPropertyChanged("SilverBages");
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        public int GoldBages
        {
            get { return _goldBages; }
            private set
            {
                if (value == _goldBages) return;
                _goldBages = value;
                OnPropertyChanged("GoldBages");
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        public string Location
        {
            get { return _location; }
            set
            {
                if (value == _location) return;
                _location = value;
                OnPropertyChanged("Location");
            }
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

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        public string DisplayName
        {
            get { return _displayName; }
            private set
            {
                if (value == _displayName) return;
                _displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }
    }
}