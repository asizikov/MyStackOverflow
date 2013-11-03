using System;
using JetBrains.Annotations;
using Microsoft.Phone.Controls;
using MyStackOverflow.Data;
using MyStackOverflow.ViewModels;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ServicesImpl
{
    internal static class ServiceLocator
    {
        public static void InitServices([NotNull] PhoneApplicationFrame rootFrame)
        {
            if (rootFrame == null) throw new ArgumentNullException("rootFrame");
            var dispatcher = new SystemDispatcher();
            dispatcher.Initialize(rootFrame.Dispatcher);
            SystemDispatcher = dispatcher;
            WebCache = new WebRequestCache();
            WebCache.PullFromStorage();
            DataProvider = new AsyncDataProvider(WebCache);
            ApplicationSettings = new ApplicationSettings();
            NavigationService = new NavigationService(rootFrame, SystemDispatcher);
            Statistics = new YandexMetricaStatistiscService();
        }

        [NotNull]
        public static ISystemDispatcher SystemDispatcher { get; private set; }

        [NotNull]
        public static AsyncDataProvider DataProvider { get; private set; }

        [NotNull]
        public static IWebCache WebCache { get; private set; }

        [NotNull]
        public static IApplicationSettings ApplicationSettings { get; private set; }

        [NotNull]
        public static INavigationService NavigationService { get; private set; }

        [NotNull]
        public static StatisticsService Statistics { get; private set; }
    }
}