using System;
using System.Linq;
using JetBrains.Annotations;
using MyStackOverflow.Data;

namespace MyStackOverflow.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private const string URL_GRAVATAR = "http://www.gravatar.com/avatar/{0}?s=128&d=identicon&r=PG";

        private readonly AsyncDataProvider _dataProvider;
        private readonly int _id;
        private string _reputation;
        private string _displayName;
        private string _location;
        private int _bronzeBages;
        private int _silverBages;
        private int _goldBages;
        private string _userPic;

        public ProfileViewModel(ISystemDispatcher dispatcher, [NotNull] AsyncDataProvider dataProvider, int id)
            : base(dispatcher)
        {
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            _dataProvider = dataProvider;
            _id = id;
            Initialize();
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
                    }
                }, ex => { IsLoading = false; });
        }

        public string UserPic
        {
            get { return _userPic; }
            set
            {
                if (value == _userPic) return;
                _userPic = string.Format(URL_GRAVATAR,value);
                OnPropertyChanged("UserPic");
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