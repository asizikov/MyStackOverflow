using JetBrains.Annotations;
using MyStackOverflow.Model.Internal;

namespace MyStackOverflow.ViewModels.Services
{
    public interface IApplicationSettings
    {
        [NotNull]
        Settings Settings { get; set; }
        void Save();
    }
}
