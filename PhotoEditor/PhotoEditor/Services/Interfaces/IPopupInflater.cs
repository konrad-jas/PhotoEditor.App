using System.Threading.Tasks;

namespace PhotoEditor.Services.Interfaces
{
    public interface IPopupInflater
    {
        Task InflatePopup(string title, string message, string confirmation);
    }
}