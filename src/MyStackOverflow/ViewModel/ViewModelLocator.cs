using System;
using MyStackOverflow.Services;
using MyStackOverflow.ViewModels.Factories;


namespace MyStackOverflow.ViewModel
{
    internal static class ViewModelLocator
    {
        private static Lazy<IMainViewModelFactory> _mainViewModelFactory;

        static ViewModelLocator()
        {
            InitFactories();
        }

        private static void InitFactories()
        {
            _mainViewModelFactory =
                new Lazy<IMainViewModelFactory>(() => new MainViewModelFactory(ServiceLocator.SystemDispatcher, ServiceLocator.DataProvider));
        }

        public static IMainViewModelFactory MainViewModelFactory
        {
            get { return _mainViewModelFactory.Value; }
        }
    }
}