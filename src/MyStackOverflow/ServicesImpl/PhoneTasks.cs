using System;
using Microsoft.Phone.Tasks;
using MyStackOverflow.ViewModels.Services;

namespace MyStackOverflow.ServicesImpl
{
    class PhoneTasks: IPhoneTasks
    {
        public void OpenUrl(string url)
        {
            var webBrowserTask = new WebBrowserTask {Uri = new Uri(url)};
            webBrowserTask.Show();
        }
    }
}
