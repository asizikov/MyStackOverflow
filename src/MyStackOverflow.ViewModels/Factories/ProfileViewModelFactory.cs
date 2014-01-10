using System;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels.Factories
{
    public class ProfileViewModelFactory : IProfileViewModelFactory
    {
        private readonly AsyncDataProvider _dataProvider;
        private readonly StatisticsService _statistics;
        private readonly IStringsProvider _stringsProvider;
        private readonly ISystemDispatcher _systemDispatcher;

        public ProfileViewModelFactory([NotNull] ISystemDispatcher systemDispatcher,
            [NotNull] AsyncDataProvider dataProvider, [NotNull] StatisticsService statistics,
            [NotNull] IStringsProvider stringsProvider)
        {
            if (systemDispatcher == null) throw new ArgumentNullException("systemDispatcher");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (statistics == null) throw new ArgumentNullException("statistics");
            if (stringsProvider == null) throw new ArgumentNullException("stringsProvider");
            _systemDispatcher = systemDispatcher;
            _dataProvider = dataProvider;
            _statistics = statistics;
            _stringsProvider = stringsProvider;
        }

        public BaseViewModel Create(int id)
        {
            return new ProfileViewModel(_systemDispatcher, _dataProvider, id, _statistics, _stringsProvider);
        }
    }
}