namespace MyStackOverflow.ViewModels.Navigation
{
    public static class NavigationParameterName
    {
        public const string Id = "id";
        public const string Questions = "questions";
        public const string Answers = "answers";
    }

    public enum DetailsType
    {
        Questions = 0,
        Answers = 1
    }
}