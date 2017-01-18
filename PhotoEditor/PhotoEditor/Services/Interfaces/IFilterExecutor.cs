using System.IO;
using PhotoEditor.Utility;

namespace PhotoEditor.Services.Interfaces
{
    public interface IFilterExecutor
    {
        IConfiguredFilterExecutor Configure(MemoryStream imageStream, FilterType filterType);
    }
}