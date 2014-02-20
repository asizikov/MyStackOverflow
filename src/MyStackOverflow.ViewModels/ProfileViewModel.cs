using System;
using System.Collections.ObjectModel;
using System.Linq;
using Curacao.MVVM.Commands;
using Curacao.MVVM.Navigation;
using Curacao.MVVM.Services;
using Curacao.MVVM.ViewModel;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.Model;
using MyStackOverflow.ViewModels.Factories;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly int _id;
        private readonly StatisticsService _statistics;
        private readonly IUserViewModelFactory _userViewModelFactory;
        private readonly INavigationService _navigation;
        private ObservableCollection<Badge> _badges;
        private UserViewModel _user;

        public ProfileViewModel(ISystemDispatcher dispatcher, [NotNull] AsyncDataProvider dataProvider, int id,
            [NotNull] StatisticsService statistics,
            [NotNull] IUserViewModelFactory userViewModelFactory, INavigationService navigation)
            : base(dispatcher)
        {
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (userViewModelFactory == null) throw new ArgumentNullException("userViewModelFactory");
            _dataProvider = dataProvider;
            _id = id;
            _statistics = statistics;
            _userViewModelFactory = userViewModelFactory;
            _navigation = navigation;
            _navigation.CleanNavigationStack();
            Initialize();
            _statistics.ReportProfilePageLoaded();
            SelectNewUserCommand=new RelayCommand(SelectNewUser);
        }

        private void SelectNewUser(object obj)
        {
            _navigation.GoToPage(Pages.LoginPage);
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
                        var user = result.Users.First();
                        User = _userViewModelFactory.Create(user);
                        LoadBagesInfo(_id);
                    }
                }, ex => { IsLoading = false; });
        }

        public RelayCommand SelectNewUserCommand { get; private set; }

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