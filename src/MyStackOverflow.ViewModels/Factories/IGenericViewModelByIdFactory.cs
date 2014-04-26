using Curacao.Mvvm.ViewModel;

namespace MyStackOverflow.ViewModels.Factories
{
    public interface IGenericViewModelByIdFactory
    {
        BaseViewModel Create(int id);
    }
}
