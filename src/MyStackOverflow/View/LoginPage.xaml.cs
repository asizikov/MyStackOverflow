using System.Windows.Navigation;
using MyStackOverflow.ViewModel;
using YAToolkit.Thombstone;

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
            if (State.Count > 0)
            {
                this.RestoreState(SearchBox);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            State.Clear();
            if (this.ShouldTombstone(e))
            {
                this.SaveState(SearchBox);
            }
        }
    }
}