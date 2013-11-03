using JetBrains.Annotations;
using MySackOverflow.Networking;
using MyStackOverflow.Model;

namespace MyStackOverflow.Data.Restful
{
    public class BagesRequest : RestfullRequest<BagesResponce>
    {
        public BagesRequest([NotNull] string baseUrl, [NotNull] ReactiveWebService webService)
            : base(baseUrl, webService)
        {

        }
    }
}