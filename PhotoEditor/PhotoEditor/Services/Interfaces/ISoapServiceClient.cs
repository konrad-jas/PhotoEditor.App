using System.Threading.Tasks;

namespace PhotoEditor.Services.Interfaces
{
	public interface ISoapServiceClient
	{
		Task<string> ChangeBrightness(string image, int alpha, int beta);
        Task<string> Dialte(string image, int size);
        Task<string> Erode(string image, int size);
        Task<string> Flip(string image, int config);
        Task<string> Gauss(string image, int value);
        Task<string> Grayscale(string image);
        Task<string> MakeBorder(string image, int top, int left, int bottom, int right);
        Task<string> Sharpen(string image);
        Task<string> Threshold(string image, int threshold, int maxVal);
	}
}