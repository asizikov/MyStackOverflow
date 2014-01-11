using System;
using System.Windows.Navigation;
using MyStackOverflow.ViewModel;
using MyStackOverflow.ViewModels.Navigation;

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
            string id = string.Empty;
            if (NavigationContext.QueryString.TryGetValue(NavigationParameterName.Id, out id))
            {
                DataContext = ViewModelLocator.ProfileViewModelFactory.Create(Int32.Parse(id));
            }
        }
    }
}