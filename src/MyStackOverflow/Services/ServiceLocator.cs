using System.Windows;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels;

namespace MyStackOverflow.Services
{
    internal static class ServiceLocator
    {
        static ServiceLocator()
        {
            InitServices();
        }

        private static void InitServices()
        {
            var dispatcher  = new SystemDispatcher();
            dispatcher.Initialize(Application.Current.RootVisual.Dispatcher);
            SystemDispatcher = dispatcher;
            WebCache = new WebRequestCache();
            WebCache.PullFromStorage();
            DataProvider = new AsyncDataProvider(WebCache);

        }

        public static ISystemDispatcher SystemDispatcher { get; private set; }
        public static AsyncDataProvider DataProvider { get; private set; }
        public static IWebCache WebCache { get; private set; }
    }
}