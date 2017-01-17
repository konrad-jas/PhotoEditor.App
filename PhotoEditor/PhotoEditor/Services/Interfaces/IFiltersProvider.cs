using System.Collections;
using System.Collections.Generic;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Services.Interfaces
{
    public interface IFiltersProvider
    {
        IEnumerable<FilterNO> GetFilters();
    }
}
