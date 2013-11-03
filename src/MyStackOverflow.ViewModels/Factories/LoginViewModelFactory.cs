using System;
using JetBrains.Annotations;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels.Factories
{
    public class LoginViewModelFactory : ILoginViewModelFactory
    {
        private readonly ISystemDispatcher _dispatcher;
        private readonly INavigationService _navigation;
        private readonly IApplicationSettings _settings;
        private readonly StatisticsService _statistics;

        public LoginViewModelFactory([NotNull] ISystemDispatcher dispatcher, [NotNull] INavigationService navigation, [NotNull] IApplicationSettings settings,
            [NotNull] StatisticsService statistics)
        {
            if (dispatcher == null) throw new ArgumentNullException("dispatcher");
            if (navigation == null) throw new ArgumentNullException("navigation");
            if (settings == null) throw new ArgumentNullException("settings");
            if (statistics == null) throw new ArgumentNullException("statistics");
            _dispatcher = dispatcher;
            _navigation = navigation;
            _settings = settings;
            _statistics = statistics;
        }

        public BaseViewModel Create()
        {
            return new LoginViewModel(_dispatcher, _navigation,_settings, _statistics );
        }
    }
}
