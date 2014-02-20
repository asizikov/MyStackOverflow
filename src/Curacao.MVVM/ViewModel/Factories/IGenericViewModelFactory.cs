using JetBrains.Annotations;

namespace Curacao.MVVM.ViewModel.Factories
{
    public interface IGenericViewModelFactory
    {
        [NotNull, PublicAPI] BaseViewModel Create();
    }
}