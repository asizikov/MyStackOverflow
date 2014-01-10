using System;
using MyStackOverflow.ServicesImpl;
using MyStackOverflow.ViewModels.Factories;

namespace MyStackOverflow.ViewModel
{
    internal static class ViewModelLocator
    {
        private static Lazy<IProfileViewModelFactory> _mainViewModelFactory;
        private static Lazy<ILoginViewModelFactory> _logingViewModelFactory;

        static ViewModelLocator()
        {
            InitFactories();
        }

        public static IProfileViewModelFactory ProfileViewModelFactory
        {
            get { return _mainViewModelFactory.Value; }
        }

        public static ILoginViewModelFactory LoginViewModelFactory
        {
            get { return _logingViewModelFactory.Value; }
        }

        private static void InitFactories()
        {
            _mainViewModelFactory =
                new Lazy<IProfileViewModelFactory>(
                    () =>
                        new ProfileViewModelFactory(ServiceLocator.SystemDispatcher, ServiceLocator.DataProvider,
                            ServiceLocator.Statistics, ServiceLocator.StringsProvider));
            _logingViewModelFactory =
                new Lazy<ILoginViewModelFactory>(
                    () =>
                        new LoginViewModelFactory(ServiceLocator.SystemDispatcher, ServiceLocator.NavigationService,
                            ServiceLocator.ApplicationSettings, ServiceLocator.Statistics));
        }
    }
}