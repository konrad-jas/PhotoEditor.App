using System.IO;
using System.Threading.Tasks;
using Android.Accounts;
using PhotoEditor.Services.Interfaces;
using Xamarin.Forms;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace PhotoEditor.Droid.Services
{
    public class ImageProvider : IImageProvider
    {
        public async Task<Stream> GetImage()
        {
            try
            {
                var device = DependencyService.Get<IDevice>();
                var picker = device.MediaPicker;
                var img = await picker.SelectPhotoAsync(new CameraMediaStorageOptions {PercentQuality = 100});
                var memoryStream = new MemoryStream();
                await img.Source.CopyToAsync(memoryStream);
                img.Dispose();
                return memoryStream;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }
    }
}