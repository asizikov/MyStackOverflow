﻿using System;
using JetBrains.Annotations;
using Microsoft.Phone.Controls;
using MyStackOverflow.ServicesImpl;
using Yandex.Metrica;

namespace MyStackOverflow
{
    internal static class Bootstrapper
    {
        public static void InitApplication([NotNull] PhoneApplicationFrame rootFrame)
        {
            if (rootFrame == null) throw new ArgumentNullException("rootFrame");
            ServiceLocator.InitServices(rootFrame);
            rootFrame.UriMapper = new MyStackOverflowUriMapper(ServiceLocator.ApplicationSettings);

            if (Configuration.EnableStatistics)
            {
                Counter.CustomAppVersion = Configuration.Version;
                Counter.TrackLocationEnabled = true;
                Counter.Start(Configuration.YandexMetricaKey);
                Counter.SendEventsBuffer();
            }
        }

        public static void SaveAppState()
        {
            ServiceLocator.ApplicationSettings.Save();
            ServiceLocator.WebCache.PushToStorage();
        }
    }
}
