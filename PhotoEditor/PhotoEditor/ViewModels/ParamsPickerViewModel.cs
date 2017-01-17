using System.Collections.Generic;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;

namespace PhotoEditor.ViewModels
{
    public class ParamsPickerViewModel : BaseViewModel
    {
        private readonly IFiltersProvider _filtersProvider;
        private FilterType _filterType;

        public ParamsPickerViewModel(IFiltersProvider filtersProvider)
        {
            _filtersProvider = filtersProvider;
        }

        public override void Init(object args)
        {
            _filterType = (FilterType) args;
            FilterOptions = _filtersProvider.GetFilterOptions(_filterType);
        }

        public IEnumerable<FilterOption> FilterOptions { get; set; }
    }
}
