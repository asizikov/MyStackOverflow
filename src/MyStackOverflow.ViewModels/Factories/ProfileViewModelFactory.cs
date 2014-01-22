using System;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels.Factories
{
    public class ProfileViewModelFactory : IGenericViewModelByIdFactory
    {
        private static Lazy<IUserViewModelFactory> _userViewModelFactory;
        private readonly AsyncDataProvider _dataProvider;
        private readonly INavigationService _navigationService;
        private readonly StatisticsService _statistics;
        private readonly IStringsProvider _stringsProvider;
        private readonly ISystemDispatcher _systemDispatcher;

        public ProfileViewModelFactory([NotNull] ISystemDispatcher systemDispatcher,
            [NotNull] AsyncDataProvider dataProvider, [NotNull] StatisticsService statistics,
            [NotNull] IStringsProvider stringsProvider, [NotNull] INavigationService navigationService)
        {
            if (systemDispatcher == null) throw new ArgumentNullException("systemDispatcher");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _systemDispatcher = systemDispatcher;
            _dataProvider = dataProvider;
            _statistics = statistics;
            _stringsProvider = stringsProvider;
            _navigationService = navigationService;


            _userViewModelFactory = new Lazy<IUserViewModelFactory>(
                () => new UserViewModelFactory(_systemDispatcher, _navigationService, _stringsProvider,
                    _statistics));
        }

        private static IUserViewModelFactory UserViewModelFactory
        {
            get { return _userViewModelFactory.Value; }
        }

        public BaseViewModel Create(int id)
        {
            return new ProfileViewModel(_systemDispatcher, _dataProvider, id, _statistics,
                UserViewModelFactory, _navigationService);
        }
    }
}