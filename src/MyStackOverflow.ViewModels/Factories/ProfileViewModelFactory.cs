using System;
using JetBrains.Annotations;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ViewModels.Factories
{
    public class ProfileViewModelFactory : IProfileViewModelFactory
    {
        private readonly ISystemDispatcher _systemDispatcher;
        private readonly AsyncDataProvider _dataProvider;
        private readonly StatisticsService _statistics;

        public ProfileViewModelFactory([NotNull] ISystemDispatcher systemDispatcher,
            [NotNull] AsyncDataProvider dataProvider, [NotNull] StatisticsService statistics)
        {
            if (systemDispatcher == null) throw new ArgumentNullException("systemDispatcher");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            if (statistics == null) throw new ArgumentNullException("statistics");
            _systemDispatcher = systemDispatcher;
            _dataProvider = dataProvider;
            _statistics = statistics;
        }

        public BaseViewModel Create(int id)
        {
            return new ProfileViewModel(_systemDispatcher, _dataProvider, id, _statistics);
        }
    }
}