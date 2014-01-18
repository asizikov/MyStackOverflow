namespace MyStackOverflow.ViewModels.Services
{
    public interface IStringsProvider
    {
        string Answers { get; }
        string Questions { get; }
        string AnswersTitle { get; }
        string QuestionsTitle { get; }
    }
}
