using System;
using System.Threading.Tasks;
using PhotoEditor.Client.ImageFilterService;
using PhotoEditor.Services.Interfaces;

namespace PhotoEditor.Services
{
    public class SoapServiceClient : AsyncServiceBase, ISoapServiceClient
    {
        private readonly ImageFilterServiceClient _client;

        public SoapServiceClient()
        {
            _client = new ImageFilterServiceClient();
        }

        public async Task<string> ChangeBrightness(string image, int alpha, int beta)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<ChangeBrightnessCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.ChangeBrightnessCompleted += handler;
            _client.ChangeBrightnessAsync(image, alpha, beta);
            return await CreateContinuationTask(completionSource.Task, () => _client.ChangeBrightnessCompleted -= handler);
        }

        public async Task<string> Dialte(string image, int size)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<DilateCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.DilateCompleted += handler;
            _client.DilateAsync(image, Convert.ToSByte(size));
            return await CreateContinuationTask(completionSource.Task, () => _client.DilateCompleted -= handler);
        }

        public async Task<string> Erode(string image, int size)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<ErodeCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.ErodeCompleted += handler;
            _client.ErodeAsync(image, Convert.ToSByte(size));
            return await CreateContinuationTask(completionSource.Task, () => _client.ErodeCompleted -= handler);
        }

        public async Task<string> Flip(string image, int config)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<FlipCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.FlipCompleted += handler;
            _client.FlipAsync(image, config);
            return await CreateContinuationTask(completionSource.Task, () => _client.FlipCompleted -= handler);
        }

        public async Task<string> Gauss(string image, int value)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<gaussFilterCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.gaussFilterCompleted += handler;
            _client.gaussFilterAsync(image, value);
            return await CreateContinuationTask(completionSource.Task, () => _client.gaussFilterCompleted -= handler);
        }

        public async Task<string> Grayscale(string image)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<GrayScaleConversionCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.GrayScaleConversionCompleted += handler;
            _client.GrayScaleConversionAsync(image);
            return
                await
                    CreateContinuationTask(completionSource.Task,
                        () => _client.GrayScaleConversionCompleted -= handler);
        }

        public async Task<string> MakeBorder(string image, int top, int left, int bottom, int right)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<MakeBorderCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.MakeBorderCompleted += handler;
            _client.MakeBorderAsync(image, top, left, bottom, right);
            return await CreateContinuationTask(completionSource.Task, () => _client.MakeBorderCompleted-= handler);
        }

        public async Task<string> Sharpen(string image)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<SharpenCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.SharpenCompleted += handler;
            _client.SharpenAsync(image);
            return await CreateContinuationTask(completionSource.Task, () => _client.SharpenCompleted -= handler);
        }

        public async Task<string> Threshold(string image, int threshold, int maxVal)
        {
            var completionSource = CreateCompletionSource<string>();
            var handler = new EventHandler<TresholdCompletedEventArgs>(
                (sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
            _client.TresholdCompleted += handler;
            _client.TresholdAsync(image, threshold, maxVal);
            return await CreateContinuationTask(completionSource.Task, () => _client.TresholdCompleted -= handler);
        }
    }
}