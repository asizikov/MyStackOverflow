using System;
using JetBrains.Annotations;
using MyStackOverflow.Networking;
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
        private const string ANSWERS_TEMPLATE = "users/{0}/answers";
        private const string USERS_FILTER_TEMPLATE = "users?filter=";


        private static string InjectIdToTemplate(string template, int id)
        {
            return string.Format(template, id);
        }

        [NotNull]
        public UsersRequest CreateUsersRequestByString(string query)
        {
            var url = URL_PREFIX + USERS_FILTER_TEMPLATE + Uri.EscapeUriString(query.ToLower());
            var request = new UsersRequest(url, _webService);
            return request;
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
            return new RestfullRequest<BagesResponce>(URL_PREFIX + InjectIdToTemplate(BAGES_TEMPLATE, userId),
                _webService);
        }

        [NotNull]
        public RestfullRequest<QuestionsResponce> CreateUserQuestionsRequestById(int userId, int page)
        {
            var url = URL_PREFIX + InjectIdToTemplate(QUESTIONS_TEMPLATE, userId);
            return new RestfullRequest<QuestionsResponce>(url + "?page=" + page + "&pagesize=30", _webService);
        }

        [NotNull]
        public RestfullRequest<AnswersResponce> CreateUserAnswersRequestById(int userId, int page)
        {
            var url = URL_PREFIX + InjectIdToTemplate(ANSWERS_TEMPLATE, userId);
            return new RestfullRequest<AnswersResponce>(url + "?page=" + page + "&pagesize=30", _webService);
        }
    }
}