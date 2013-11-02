using System;
using System.Globalization;
using System.Windows.Navigation;
using JetBrains.Annotations;
using MyStackOverflow.ViewModels.Navigation;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow
{
    internal sealed class MyStackOverflowUriMapper : UriMapperBase
    {
        private readonly IApplicationSettings _applicationSettings;

        public MyStackOverflowUriMapper([NotNull] IApplicationSettings applicationSettings)
        {
            if (applicationSettings == null) throw new ArgumentNullException("applicationSettings");
            _applicationSettings = applicationSettings;
        }

        public override Uri MapUri(Uri uri)
        {
            if (!uri.OriginalString.Contains(Pages.EntryPage))
            {
                return uri;
            }

            string updatedUri;

            if (_applicationSettings.Settings.Me != null)
            {
                updatedUri = string.Format("{0}?id={1}",
                    Pages.ProfilePage, _applicationSettings.Settings.Me.Id.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                updatedUri = Pages.LoginPage;
            }

            return new Uri(updatedUri, UriKind.Relative);
        }
    }
}