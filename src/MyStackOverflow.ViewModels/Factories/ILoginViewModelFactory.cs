using JetBrains.Annotations;

namespace MyStackOverflow.ViewModels.Factories
{
    public interface ILoginViewModelFactory
    {
        [NotNull]
        BaseViewModel Create();
    }
}
