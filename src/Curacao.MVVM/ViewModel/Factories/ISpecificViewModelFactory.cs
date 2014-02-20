using JetBrains.Annotations;

namespace Curacao.MVVM.ViewModel.Factories
{
    public interface ISpecificViewModelFactory<out T> where T : BaseViewModel
    {
        [NotNull, PublicAPI] T Create();
    }
}