using System.IO;
using System.Threading.Tasks;

namespace PhotoEditor.Services.Interfaces
{
    public interface IConfiguredFilterExecutor
    {
        IConfiguredFilterExecutor AddParameter(int parameter);
        Task<Stream> ExecuteFilter();
    }
}