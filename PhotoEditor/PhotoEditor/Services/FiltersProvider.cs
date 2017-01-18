using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Services
{
    public class FiltersProvider : IFiltersProvider
    {
        private readonly Dictionary<FilterType, IEnumerable<string>> _filtersOptions;

        public FiltersProvider()
        {
            _filtersOptions = new Dictionary<FilterType, IEnumerable<string>>
            {
                {FilterType.ChangeBrightness, new[] { "Alpha", "Beta" }},
                {FilterType.Dilate, new []{ "Size"}},
                {FilterType.Erode, new [] {"Size"} },
                {FilterType.Flip, new [] {"Config"} },
                {FilterType.Gauss, new [] {"Value"} },
                {FilterType.GrayScale, new string[0] },
                {FilterType.MakeBorder, new [] { "Top","Left", "Bottom", "Right" }},
                {FilterType.Sharpen, new string[0] },
                {FilterType.Threshold, new [] { "Threshold", "MaxVal" }}
            };
        }

        public IEnumerable<FilterNO> GetFilters()
        {
            return new List<FilterNO>
            {
                new FilterNO { Type = FilterType.ChangeBrightness, Name = "Change brightness"},
                new FilterNO { Type = FilterType.Dilate, Name = "Dilate"},
                new FilterNO { Type = FilterType.Erode, Name = "Erode"},
                new FilterNO { Type = FilterType.Flip, Name = "Flip"},
                new FilterNO { Type = FilterType.Gauss, Name = "Gaussian filter"},
                new FilterNO { Type = FilterType.GrayScale, Name = "Grayscale"},
                new FilterNO { Type = FilterType.MakeBorder, Name = "Make border"},
                new FilterNO { Type = FilterType.Sharpen, Name = "Sharpen"},
                new FilterNO { Type = FilterType.Threshold, Name = "Threshold"},
            };
        }

        public IEnumerable<FilterOption> GetFilterOptions(FilterType filterType)
        {
            return _filtersOptions[filterType].Select(opt => new FilterOption {Name = opt}).ToList();
        }
    }
}