using System;
using Curacao.Mvvm.Abstractions.Navigation;
using Curacao.Mvvm.Abstractions.Services;
using JetBrains.Annotations;
using MyStackOverflow.Model;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels.Factories
{
    public class UserViewModelFactory : IUserViewModelFactory
    {
        private readonly ISystemDispatcher _dispatcher;
        private readonly INavigationService _navigation;
        private readonly StatisticsService _statistics;
        private readonly IStringsProvider _stringsProvider;

        public UserViewModelFactory([NotNull] ISystemDispatcher dispatcher, [NotNull] INavigationService navigation,
            [NotNull] IStringsProvider stringsProvider,
            [NotNull] StatisticsService statistics)
        {
            if (dispatcher == null) throw new ArgumentNullException("dispatcher");
            if (navigation == null) throw new ArgumentNullException("navigation");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            if (statistics == null) throw new ArgumentNullException("statistics");
            _dispatcher = dispatcher;
            _navigation = navigation;
            _stringsProvider = stringsProvider;
            _statistics = statistics;
        }


        public UserViewModel Create(User user)
        {
            return new UserViewModel(_dispatcher, user, _stringsProvider, _navigation);
        }
    }
}