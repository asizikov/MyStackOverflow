using System;

namespace MyStackOverflow
{
    public static class Configuration
    {
        public static bool EnableStatistics
        {
            get { return false; }
        }

        public static Version Version
        {
            get { return new Version(1, 2, 1); }
        }

        public static uint YandexMetricaKey
        {
            get { return 0; }
        }
    }
}