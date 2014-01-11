using System;
using System.Windows.Navigation;
using MyStackOverflow.ViewModel;
using MyStackOverflow.ViewModels.Navigation;

namespace MyStackOverflow.View
{
    public partial class QuestionsPage
    {
        public QuestionsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var id = string.Empty;
            if (NavigationContext.QueryString.TryGetValue(NavigationParameterName.Id, out id))
            {
                DataContext = ViewModelLocator.QuestionsViewModelFactory.Create(Int32.Parse(id));
            }
        }
    }
}