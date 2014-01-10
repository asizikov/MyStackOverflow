using JetBrains.Annotations;
using MySackOverflow.Networking;
using MyStackOverflow.Model;

namespace MyStackOverflow.Data.Restful
{
    public class RestfulCallFactory
    {
        [NotNull] private readonly ReactiveWebService _webService = new ReactiveWebService();

        private const string URL_PREFIX = "http://api.stackoverflow.com/1.1/";
        private const string USERS_TEMPLATE = "users/{0}";
        private const string BAGES_TEMPLATE = "users/{0}/badges";
        private const string QUESTIONS_TEMPLATE = "users/{0}/questions";


        private static string InjectIdToTemplate(string template, int id)
        {
            return string.Format(template, id);
        }

        [NotNull]
        public UsersRequest CreateUsersRequestById(int userId)
        {
            var request = new UsersRequest(URL_PREFIX + InjectIdToTemplate(USERS_TEMPLATE, userId), _webService);
            return request;
        }

        [NotNull]
        public RestfullRequest<BagesResponce> CreateBagesRequestById(int userId)
        {
            return new BagesRequest(URL_PREFIX + InjectIdToTemplate(BAGES_TEMPLATE, userId), _webService);
        }

        [NotNull]
        public RestfullRequest<QuestionsResponce> CreateUserQuestionsRequestById(int userId)
        {
            return new QuestionsRequest(URL_PREFIX + InjectIdToTemplate(QUESTIONS_TEMPLATE, userId), _webService);
        }
    }
}