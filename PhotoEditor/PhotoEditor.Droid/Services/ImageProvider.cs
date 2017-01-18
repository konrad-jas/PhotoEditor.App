using System;
using System.IO;
using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;
using XLabs.Platform.Services.Media;

namespace PhotoEditor.Droid.Services
{
    public class ImageProvider : IImageProvider
    {
        private readonly IMediaPicker _mediaPicker;

        public ImageProvider(IMediaPicker mediaPicker)
        {
            _mediaPicker = mediaPicker;
        }

        public async Task<Stream> GetImage()
        {
            try
            {
                var picker = _mediaPicker;
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