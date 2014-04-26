using System;
using Curacao.Mvvm.Abstractions.Navigation;
using Curacao.Mvvm.Abstractions.Services;
using Curacao.Mvvm.ViewModel;
using Curacao.Mvvm.ViewModel.Factories;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels.Factories
{
    public class LoginViewModelFactory : IGenericViewModelFactory
    {
        private readonly ISystemDispatcher _dispatcher;
        private readonly INavigationService _navigation;
        private readonly IApplicationSettings _settings;
        private readonly AsyncDataProvider _dataProvider;
        private readonly StatisticsService _statistics;

        public LoginViewModelFactory([NotNull] ISystemDispatcher dispatcher, [NotNull] INavigationService navigation,
            [NotNull] IApplicationSettings settings, AsyncDataProvider dataProvider,
            [NotNull] StatisticsService statistics)
        {
            if (dispatcher == null) throw new ArgumentNullException("dispatcher");
            if (navigation == null) throw new ArgumentNullException("navigation");
            if (settings == null) throw new ArgumentNullException("settings");
            if (statistics == null) throw new ArgumentNullException("statistics");
            _dispatcher = dispatcher;
            _navigation = navigation;
            _settings = settings;
            _dataProvider = dataProvider;
            _statistics = statistics;
        }

        public BaseViewModel Create()
        {
            return new LoginViewModel(_dispatcher, _navigation, _settings, _dataProvider, _statistics);
        }
    }
}