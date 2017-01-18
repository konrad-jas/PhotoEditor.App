using System.IO;
using System.Threading.Tasks;

namespace PhotoEditor.Services.Interfaces
{
    public interface IStandardExecutor
    {
        IConfiguredFilterExecutor WithParameter(int parameter);
        Task<Stream> ExecuteFilter();
    }
}