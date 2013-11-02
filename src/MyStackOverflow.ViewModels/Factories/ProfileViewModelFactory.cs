using System;
using JetBrains.Annotations;
using MyStackOverflow.Data;

namespace MyStackOverflow.ViewModels.Factories
{
    public class ProfileViewModelFactory : IProfileViewModelFactory
    {
        private readonly ISystemDispatcher _systemDispatcher;
        private readonly AsyncDataProvider _dataProvider;

        public ProfileViewModelFactory([NotNull] ISystemDispatcher systemDispatcher,
            [NotNull] AsyncDataProvider dataProvider)
        {
            if (systemDispatcher == null) throw new ArgumentNullException("systemDispatcher");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            _systemDispatcher = systemDispatcher;
            _dataProvider = dataProvider;
        }

        public BaseViewModel Create()
        {
            return new ProfileViewModel(_systemDispatcher,_dataProvider);
        }
    }
}