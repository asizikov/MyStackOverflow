using MyStackOverflow.ViewModels.Services;
using Yandex.Metrica;

namespace MyStackOverflow.ServicesImpl
{
    internal class YandexMetricaStatistiscService : StatisticsService
    {
        public override void PublishEvent(string eventName)
        {
            Counter.ReportEvent(eventName);
        }
    }
}