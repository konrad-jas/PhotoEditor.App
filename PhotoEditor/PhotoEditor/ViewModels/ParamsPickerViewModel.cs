using System.Collections;
using System.Collections.Generic;

namespace PhotoEditor.ViewModels
{
    public class ParamsPickerViewModel : BaseViewModel
    {
        public IEnumerable<FilterOption> FilterOptions { get; set; }
    }
}
