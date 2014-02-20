using System;
using Curacao.MVVM.ViewModel.Factories;
using MyStackOverflow.ServicesImpl;
using MyStackOverflow.ViewModels.Factories;

namespace MyStackOverflow.ViewModel
{
    internal static class ViewModelLocator
    {
        private static Lazy<IGenericViewModelByIdFactory> _mainViewModelFactory;
        private static Lazy<IGenericViewModelFactory> _logingViewModelFactory;
        private static Lazy<QuestionsViewModelFactory> _questionsViewModelFactory;


        static ViewModelLocator()
        {
            InitFactories();
        }

        public static IGenericViewModelByIdFactory ProfileViewModelFactory
        {
            get { return _mainViewModelFactory.Value; }
        }

        public static IGenericViewModelFactory LoginViewModelFactory
        {
            get { return _logingViewModelFactory.Value; }
        }

        public static QuestionsViewModelFactory QuestionsViewModelFactory
        {
            get { return _questionsViewModelFactory.Value; }
        }

        private static void InitFactories()
        {
            _mainViewModelFactory =
                new Lazy<IGenericViewModelByIdFactory>(
                    () =>
                        new ProfileViewModelFactory(ServiceLocator.SystemDispatcher, ServiceLocator.DataProvider,
                            ServiceLocator.Statistics, ServiceLocator.StringsProvider, ServiceLocator.NavigationService));
            _logingViewModelFactory =
                new Lazy<IGenericViewModelFactory>(
                    () =>
                        new LoginViewModelFactory(ServiceLocator.SystemDispatcher, ServiceLocator.NavigationService,
                            ServiceLocator.ApplicationSettings, ServiceLocator.DataProvider, ServiceLocator.Statistics));

            _questionsViewModelFactory = new Lazy<QuestionsViewModelFactory>(() =>
                new QuestionsViewModelFactory(ServiceLocator.SystemDispatcher, ServiceLocator.Statistics,
                    ServiceLocator.DataProvider, ServiceLocator.StringsProvider, ServiceLocator.PhoneTasks)
                );
        }
    }
}