using System.Windows.Navigation;
using MyStackOverflow.ViewModel;

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
            DataContext = ViewModelLocator.ProfileViewModelFactory.Create();
        }
    }


}