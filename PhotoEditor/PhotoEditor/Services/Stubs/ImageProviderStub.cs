using PhotoEditor.Services.Interfaces;

namespace PhotoEditor.Services.Stubs
{
	public class ImageProviderStub : IImageProvider
	{
		public string GetImagePath()
		{
			return string.Empty;
		}
	}
}