namespace MyStackOverflow.ViewModels.Services
{
    public abstract class StatisticsService
    {
        private const string  PROFILE_PAGE_LOADED = "Profile page loaded";
        private const string  ACTIVITY_LOADED = "Activity page loaded:";
        private const string LOGIN_PAGE_LOADED = "Login page loaded";

        protected abstract void PublishEvent(string eventName);

        public void PublishLoginPageLoaded()
        {
            PublishEvent(LOGIN_PAGE_LOADED);
        }

        public void ReportProfilePageLoaded()
        {
            PublishEvent(PROFILE_PAGE_LOADED);
        }

        public void PublishActivityPageLoaded(bool isQuestions)
        {
            PublishEvent(ACTIVITY_LOADED + (isQuestions ? "Questions" : "Answers"));
        }
    }
}
