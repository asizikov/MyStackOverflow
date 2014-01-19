using JetBrains.Annotations;
using MyStackOverflow.Networking;
using MyStackOverflow.Model;

namespace MyStackOverflow.Data.Restful
{
    public class UsersRequest : RestfullRequest<UsersResponce>
    {
        public UsersRequest([NotNull] string baseUrl, [NotNull] ReactiveWebService webService)
            : base(baseUrl, webService)
        {

        }
    }
}