using System.IO.IsolatedStorage;
using JetBrains.Annotations;
using MyStackOverflow.Model.Internal;
using MyStackOverflow.ViewModels.Services;
using Newtonsoft.Json;

namespace MyStackOverflow.Services
{
    public class ApplicationSettings : IApplicationSettings
    {
        private const string KEY = "Settings";

        public ApplicationSettings()
        {
            Settings = InitSettings();
        }


        [NotNull, Pure]
        private static Settings InitSettings()
        {
            Settings settings;
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(KEY))
            {
                settings = GetEmptySettings();
                IsolatedStorageSettings.ApplicationSettings.Add(KEY, SerializeToStrng(settings));
            }
            else
            {
                var meJsonString = (string)IsolatedStorageSettings.ApplicationSettings[KEY];
                settings = DeserializeFromString(meJsonString);
            }
            return settings;
        }

        [NotNull, Pure]
        private static Settings DeserializeFromString(string favsJsonString)
        {
            var deserializedSettings = JsonConvert.DeserializeObject<Settings>(favsJsonString);
            return deserializedSettings ?? GetEmptySettings();
        }

        private static string SerializeToStrng(Settings favs)
        {
            return JsonConvert.SerializeObject(favs);
        }

        [NotNull, Pure]
        private static Settings GetEmptySettings()
        {
            return new Settings();;
        }

        public Settings Settings { get; set; }

        public void Save()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(KEY))
            {
                IsolatedStorageSettings.ApplicationSettings.Add(KEY, SerializeToStrng(Settings));
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings[KEY] = SerializeToStrng(Settings);
            }
        }
    }
}