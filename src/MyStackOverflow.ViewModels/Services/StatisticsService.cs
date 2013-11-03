namespace MyStackOverflow.ViewModels.Services
{
    public abstract class StatisticsService
    {
        private const string LOGIN_PAGE_LOADED = "Login page loaded";
        public void PublishLoginPageLoaded()
        {
            PublishEvent(LOGIN_PAGE_LOADED);
        }


        public abstract void PublishEvent(string eventName);
    }
}
