using JetBrains.Annotations;
using MySackOverflow.Networking;

namespace MyStackOverflow.Data.Restful
{
    public class RestfulCallFactory
    {
        [NotNull] private readonly ReactiveWebService _webService = new ReactiveWebService();

        private const string URL_PREFIX = "http://api.stackoverflow.com/1.1/";
        private const string USERS_TEMPLATE = "users/{0}";


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
    }
}