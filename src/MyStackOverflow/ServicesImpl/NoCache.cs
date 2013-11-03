using MyStackOverflow.Data;

namespace MyStackOverflow.ServicesImpl
{
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