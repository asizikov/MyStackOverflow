using System.Windows.Navigation;
using MyStackOverflow.ViewModel;

namespace MyStackOverflow
{
    public partial class MainPage
    {
        public MainPage()
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