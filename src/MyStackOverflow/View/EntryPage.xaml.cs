using System;
using System.Windows.Navigation;

namespace MyStackOverflow.View
{
    public partial class EntryPage
    {
        public EntryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            throw new Exception("we should never ever navigate here");
        }
    }
}