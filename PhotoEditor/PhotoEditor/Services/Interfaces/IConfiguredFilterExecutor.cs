using PhotoEditor.Utility;

namespace PhotoEditor.Services.Interfaces
{
    public interface IConfiguredFilterExecutor
    {
        IStandardExecutor ForFilter(FilterType filterType);
        ICompositeFilterExecutor ForCompositeFilter();
    }
}