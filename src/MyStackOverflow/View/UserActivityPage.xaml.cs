using System;
using System.Windows.Navigation;
using MyStackOverflow.ViewModel;
using MyStackOverflow.ViewModels.Navigation;

namespace MyStackOverflow.View
{
    public partial class UserActivityPage
    {
        public UserActivityPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string id;
            string  type;
            if (NavigationContext.QueryString.TryGetValue(NavigationParameterName.Id, out id))
            {
                DataContext = ViewModelLocator.QuestionsViewModelFactory.Create(Int32.Parse(id), GetDetailsType());
            }
        }

        private DetailsType GetDetailsType()
        {
            return NavigationContext.QueryString.ContainsKey(NavigationParameterName.Answers)
                ? DetailsType.Answers
                : DetailsType.Questions;
        }
    }
}