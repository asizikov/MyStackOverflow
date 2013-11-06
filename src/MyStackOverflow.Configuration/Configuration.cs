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
            get { return new Version(0, 1, 1); }
        }

        public static uint YandexMetricaKey
        {
            get { return 0; }
        }
    }
}