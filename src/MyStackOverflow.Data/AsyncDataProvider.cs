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

        public IObservable<BagesResponce> GetUserBagesList(int id)
        {
            var request = CallFactory.CreateBagesRequestById(id);
            return GetDataAsync(request);
        }

        public IObservable<QuestionsResponce> GetUserQuestionsList(int id)
        {
            var request = CallFactory.CreateUserQuestionsRequestById(id);
            return GetDataAsync(request);
        }
    }
}