using System.Collections.Generic;
using JetBrains.Annotations;

namespace MyStackOverflow.ViewModels.Navigation
{
    public interface INavigationService
    {
        void GoBack();
        bool CanGoBack();
        void GoToPage(string page, IEnumerable<NavigationParameter> parameters = null);
        void CleanNavigationStack();
        void GoToPage(string page, [CanBeNull] IEnumerable<NavigationParameter> parameters, int numberOfItemsToRemove);
    }
}
