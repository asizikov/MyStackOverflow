using JetBrains.Annotations;
using MyStackOverflow.Model;

namespace MyStackOverflow.ViewModels.Factories
{
    public interface IUserViewModelFactory
    {
        [NotNull]
        UserViewModel Create(User user);
    }
}