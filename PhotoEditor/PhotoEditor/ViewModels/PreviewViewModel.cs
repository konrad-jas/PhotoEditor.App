using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;

namespace PhotoEditor.ViewModels
{
	public class PreviewViewModel : BaseViewModel
	{
	    private readonly IMediaPicker _mediaPicker;

	    public PreviewViewModel(IMediaPicker mediaPicker)
		{
	        _mediaPicker = mediaPicker;
	        ChooseImageCommand = new Command(ChooseImageAction);
		}

		public ImageSource ImageSource { get; set; }

		public Command ChooseImageCommand { get; private set; }
		private async void ChooseImageAction()
		{
		    try
		    {
		        var media = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions {PercentQuality = 50});
		        ImageSource = ImageSource.FromStream(() =>
		        {
		            var stream = media.Source;
		            media.Dispose();
		            return stream;
		        });
                RaisePropertyChanged(() => ImageSource);
		    }
		    catch (TaskCanceledException)
		    {
		    }
		}
	}
}
