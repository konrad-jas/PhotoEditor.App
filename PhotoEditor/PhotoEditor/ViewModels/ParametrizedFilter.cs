using System.Collections.Generic;
using System.Windows.Input;
using PhotoEditor.Utility;
using Xamarin.Forms;

namespace PhotoEditor.ViewModels
{
    public class ParametrizedFilter
    {
        public string Name { get; set; }
        public FilterType FilterType { get; set; }
        public IEnumerable<FilterOption> Options { get; set; } 
        public ICommand Command { get; set; }
        public ICommand RemoveCommand { get; set; }
    }
}
