using System;
using System.Windows;
using JetBrains.Annotations;
using Microsoft.Phone.Controls;
using MySackOverflow.Networking;
using MyStackOverflow.Model;

namespace MyStackOverflow
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var webservice = new ReactiveWebService();
            var url = "http://api.stackoverflow.com/1.1/users/555014";

            var requesr = new UserRequest(url, webservice).Execute()
                .Subscribe(result =>
                {
                    var r = result.total;
                }, ex => {});

        }
    }

    public class UserRequest : RestfullRequest<RootObject>
    {
        public UserRequest([NotNull] string baseUrl, [NotNull] ReactiveWebService webService)
            : base(baseUrl, webService)
        {

        }
    }
}