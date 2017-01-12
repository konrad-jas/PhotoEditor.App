using PhotoEditor.Services.Interfaces;
using Xamarin.Forms;

namespace PhotoEditor.ViewModels
{
	public class PreviewViewModel : BaseViewModel
	{
		private readonly IImageProvider _imageProvider;

		public PreviewViewModel(IImageProvider imageProvider)
		{
			_imageProvider = imageProvider;
			ChooseImageCommand = new Command(ChooseImageAction);
		}

		public string ImagePath { get; set; }

		public Command ChooseImageCommand { get; private set; }
		private void ChooseImageAction()
		{
			ImagePath = _imageProvider.GetImagePath();
			RaisePropertyChanged(() => ImagePath);
		}
	}
}
