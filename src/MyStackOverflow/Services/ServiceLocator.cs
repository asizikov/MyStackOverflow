using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            DataProvider = new AsyncDataProvider(new  NoCache());
        }

        public static ISystemDispatcher SystemDispatcher { get; private set; }
        public static AsyncDataProvider DataProvider { get; private set; }
    }

    public sealed class NoCache : IWebCache
    {
        public bool IsCached<T>(string url) where T : new()
        {
            return false;
        }

        public void Put<T>(T item, string url) where T : new()
        {
            ;
        }

        public T Fetch<T>(string url) where T : new()
        {
            return default(T);
        }

        public void PullFromStorage()
        {
            ;
        }

        public void PushToStorage()
        {
            ;
        }
    }
}