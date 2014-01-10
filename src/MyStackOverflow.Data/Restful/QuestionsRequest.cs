using JetBrains.Annotations;
using MySackOverflow.Networking;
using MyStackOverflow.Model;

namespace MyStackOverflow.Data.Restful
{
    public class QuestionsRequest : RestfullRequest<QuestionsResponce>
    {
        public QuestionsRequest([NotNull] string baseUrl, [NotNull] ReactiveWebService webService)
            : base(baseUrl, webService)
        {

        }
    }
}