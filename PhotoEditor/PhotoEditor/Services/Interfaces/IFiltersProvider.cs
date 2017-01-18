using System.Collections;
using System.Collections.Generic;
using PhotoEditor.Utility;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Services.Interfaces
{
    public interface IFiltersProvider
    {
        IEnumerable<FilterNO> GetFilters();
        IEnumerable<FilterOption> GetFilterOptions(FilterType filterType);
    }
}
