using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Services.Interfaces
{
    public interface ICompositeFilterExecutor
    {
        Task<Stream> ExecuteFilter(IEnumerable<ParametrizedFilter> filters);
    }
}