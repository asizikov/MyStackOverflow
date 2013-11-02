using System.Windows.Navigation;
using MyStackOverflow.ViewModel;

namespace MyStackOverflow.View
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = ViewModelLocator.LoginViewModelFactory.Create();
        }
    }
}