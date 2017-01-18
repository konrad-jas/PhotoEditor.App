using System;
using System.Collections.Generic;
using System.Text;
using PhotoEditor.Utility;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Services
{
    /// <summary>
    /// This abomination should never exist, unfortunately 
    /// backend parses data in a really awkward way
    /// </summary>
    public static class CompositeFilterBuilder
    {
        private static readonly Dictionary<FilterType, string> _filterNames = new Dictionary<FilterType, string>
        {
            {FilterType.ChangeBrightness, "changeBrightness" },
            {FilterType.Threshold, "treshold"},
            {FilterType.Gauss, "gaussFilter" },
            {FilterType.MakeBorder, "MakeBorder" },
            {FilterType.GrayScale, "GrayScaleConversion" }
        };

        private static readonly Dictionary<string, string> _filterOptions = new Dictionary<string, string>
        {
            
        } 

        public static string Build(IEnumerable<ParametrizedFilter> filters)
        {
            var stringBuilder = new StringBuilder().Append("<filters>");
            foreach (var filter in filters)
            {
                var filterName = _filterNames.ContainsKey(filter.FilterType)
                    ? _filterNames[filter.FilterType]
                    : filter.Name;
                stringBuilder.Append($@"<filter name='{filterName}'>");
                foreach (var option in filter.Options)
                {
                    var optionName = option.Name.ToLower();
                    if (optionName == "threshold")
                        optionName = "treshold";
                    else if (optionName == "config")
                        optionName = "flipCode";

                    stringBuilder.Append($@"<{optionName}>{option.Value}</{optionName}>");
                }
                stringBuilder.Append($"</{filterName}");
            }
            return stringBuilder.Append("</filters>").ToString();
        }
    }
}