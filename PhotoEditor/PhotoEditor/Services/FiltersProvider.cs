using System.Collections.Generic;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Services
{
    public class FiltersProvider : IFiltersProvider
    {
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
    }
}