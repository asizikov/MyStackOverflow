
using System;
using JetBrains.Annotations;
using MyStackOverflow.Data;

namespace MyStackOverflow.ViewModels.Factories
{
    public interface IMainViewModelFactory
    {
        BaseViewModel Create();
    }

    public class MainViewModelFactory : IMainViewModelFactory
    {
        private readonly ISystemDispatcher _systemDispatcher;
        private readonly AsyncDataProvider _dataProvider;

        public MainViewModelFactory([NotNull] ISystemDispatcher systemDispatcher,
            [NotNull] AsyncDataProvider dataProvider)
        {
            if (systemDispatcher == null) throw new ArgumentNullException("systemDispatcher");
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            _systemDispatcher = systemDispatcher;
            _dataProvider = dataProvider;
        }

        public BaseViewModel Create()
        {
            return new MainViewModel(_systemDispatcher,_dataProvider);
        }
    }
}
