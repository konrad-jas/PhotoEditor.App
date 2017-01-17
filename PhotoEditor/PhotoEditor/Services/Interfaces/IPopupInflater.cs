using System.Threading.Tasks;
using PhotoEditor.Utility;

namespace PhotoEditor.Services.Interfaces
{
    public interface IPopupInflater
    {
        Task InflatePopup(string title, string message, string confirmation);
        Task ShowParamsPicker(FilterType filter);
        Task ClosePopup();
    }
}