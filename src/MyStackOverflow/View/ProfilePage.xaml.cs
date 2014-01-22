using System;
using System.Windows.Navigation;
using MyStackOverflow.ServicesImpl;
using MyStackOverflow.ViewModel;
using MyStackOverflow.ViewModels.Navigation;
using YAToolkit.Thombstone;

namespace MyStackOverflow.View
{
    public partial class ProfilePage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var id = string.Empty;
            if (NavigationContext.QueryString.TryGetValue(NavigationParameterName.Id, out id))
            {
                DataContext = ViewModelLocator.ProfileViewModelFactory.Create(Int32.Parse(id));
            }
            if (State.Count > 0)
            {
                this.RestoreState(ProfilePivot);
            }
        }

        private void ApplicationBarMenuItem_OnClick(object sender, EventArgs e)
        {
            ServiceLocator.ApplicationSettings.Settings.Me = null;
            ServiceLocator.ApplicationSettings.Save();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            State.Clear();
            if (this.ShouldTombstone(e))
            {
                this.SaveState(ProfilePivot);
            }
        }
    }
}