using System;
using System.Windows.Input;
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

        public LoginViewModel([NotNull] ISystemDispatcher dispatcher, [NotNull] INavigationService navigation,
            [NotNull] IApplicationSettings settings)
            : base(dispatcher)
        {
            if (navigation == null) throw new ArgumentNullException("navigation");
            if (settings == null) throw new ArgumentNullException("settings");
            _navigation = navigation;
            _settings = settings;
            InitCommands();
        }

        private void InitCommands()
        {
            GoToProfileCommand = new RelayCommand(GoToProfile);
        }

        [UsedImplicitly(ImplicitUseKindFlags.Default)]
        public string UserId { get; set; }

        [UsedImplicitly(ImplicitUseKindFlags.Access)]
        public ICommand GoToProfileCommand { get; private set; }

        private void GoToProfile(object obj)
        {
            _navigation.GoToPage(Pages.ProfilePage);
        }

    }
}