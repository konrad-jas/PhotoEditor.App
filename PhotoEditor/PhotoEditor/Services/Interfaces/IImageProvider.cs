using System.IO;
using System.Threading.Tasks;

namespace PhotoEditor.Services.Interfaces
{
	public interface IImageProvider
	{
		Task<Stream> GetImage();
	}
}