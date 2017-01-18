using PhotoEditor.Utility;
using Xamarin.Forms;

namespace PhotoEditor.ViewModels
{
    public class FilterNO
    {
        public string Name { get; set; }
        public Command FilterCommand { get; set; }
        public FilterType Type { get; set; }
    }
}
