using System.Threading.Tasks;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Services.Interfaces
{
    public interface INavigator
    {
        Task InflatePopup(string title, string message, string confirmation);
        Task ShowViewModel<TViewModel>(object args) where TViewModel : BaseViewModel;
        Task GoBack();
    }
}