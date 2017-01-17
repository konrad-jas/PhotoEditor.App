using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using PhotoEditor.Services.Interfaces;

namespace PhotoEditor.WP.Services
{
    public class ImageProvider : IImageProvider
    {
        public Task<Stream> GetImage()
        {
            var completionSource = new TaskCompletionSource<Stream>();
            var chooser = new PhotoChooserTask {ShowCamera = false};
            chooser.Completed += async (sender, result) =>
            {
                var memoryStream = new MemoryStream();
                await result.ChosenPhoto.CopyToAsync(memoryStream);
                result.ChosenPhoto.Dispose();
                memoryStream.Position = 0;
                completionSource.SetResult(memoryStream);
            };
            chooser.Show();
            return completionSource.Task;
        }
    }
}
