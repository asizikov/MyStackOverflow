using System;
using JetBrains.Annotations;
using MyStackOverflow.Model;

namespace MyStackOverflow.Data
{
    public class AsyncDataProvider : BaseAsyncWebClient
    {
        public AsyncDataProvider([NotNull] IWebCache cache)
            : base(cache)
        {
        }

        public IObservable<UsersResponce> GetUsersAsync(int userId)
        {
            var request = CallFactory.CreateUsersRequestById(userId);
            return GetDataAsync(request);
        }
    }
}