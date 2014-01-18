using MyStackOverflow.Resources;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ServicesImpl
{
    internal class StringsProvider : IStringsProvider
    {
        public string Answers
        {
            get { return AppResources.Answers; }
        }

        public string Questions
        {
            get { return AppResources.Questions; }
        }

        public string AnswersTitle
        {
            get { return AppResources.AnswersTitle; }
        }

        public string QuestionsTitle
        {
            get { return AppResources.QuestionsTitle; }
        }
    }
}