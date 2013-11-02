using System;
using MyStackOverflow.Services;
using MyStackOverflow.ViewModels.Factories;


namespace MyStackOverflow.ViewModel
{
    internal static class ViewModelLocator
    {
        private static Lazy<IProfileViewModelFactory> _mainViewModelFactory;

        static ViewModelLocator()
        {
            InitFactories();
        }

        private static void InitFactories()
        {
            _mainViewModelFactory =
                new Lazy<IProfileViewModelFactory>(() => new ProfileViewModelFactory(ServiceLocator.SystemDispatcher, ServiceLocator.DataProvider));
        }

        public static IProfileViewModelFactory ProfileViewModelFactory
        {
            get { return _mainViewModelFactory.Value; }
        }
    }
}