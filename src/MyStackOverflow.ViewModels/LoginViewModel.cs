using System;
using JetBrains.Annotations;
using MyStackOverflow.ViewModels.Commands;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IApplicationSettings _settings;
        private readonly StatisticsService _statistics;
        private string _userId;

        public LoginViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] INavigationService navigation,
            [NotNull] IApplicationSettings settings,
            [NotNull] StatisticsService statistics)
            : base(dispatcher)
        {
            if (navigation == null) throw new ArgumentNullException("navigation");
            if (settings == null) throw new ArgumentNullException("settings");
            if (statistics == null) throw new ArgumentNullException("statistics");
            _navigation = navigation;
            _settings = settings;
            _statistics = statistics;
            InitCommands();
            _statistics.PublishLoginPageLoaded();
        }

        [UsedImplicitly(ImplicitUseKindFlags.Default)]
        public string UserId
        {
            get { return _userId; }
            set
            {
                if (value == _userId) return;
                _userId = value;
                GoToProfileCommand.RaisCanExecuteChanged();
                OnPropertyChanged("UserId");
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        public RelayCommand GoToProfileCommand { get; private set; }

        private void InitCommands()
        {
            GoToProfileCommand = new RelayCommand(GoToProfile, CanExecute);
        }

        private bool CanExecute(object o)
        {
            return !string.IsNullOrWhiteSpace(UserId);
        }

        private void GoToProfile(object obj)
        {
            _navigation.GoToPage(Pages.ProfilePage, new[]
            {
                new NavigationParameter
                {
                    Parameter = NavigationParameterName.Id,
                    Value = UserId
                }
            });
        }
    }
}