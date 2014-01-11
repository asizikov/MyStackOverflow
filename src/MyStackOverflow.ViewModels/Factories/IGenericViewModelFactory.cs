using JetBrains.Annotations;

namespace MyStackOverflow.ViewModels.Factories
{
    public interface IGenericViewModelFactory
    {
        [NotNull]
        BaseViewModel Create();
    }
}
