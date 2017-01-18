using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;
using PhotoEditor.ViewModels;

namespace PhotoEditor.Services
{
    public class FilterExecutor : IFilterExecutor, IConfiguredFilterExecutor, IStandardExecutor, ICompositeFilterExecutor
    {
        private readonly ISoapServiceClient _soapServiceClient;
        private readonly List<int> _parameters;
        private string _image;
        private FilterType _filterType;

        public FilterExecutor(ISoapServiceClient soapServiceClient)
        {
            _soapServiceClient = soapServiceClient;
            _parameters = new List<int>();
        }

        public IConfiguredFilterExecutor Configure(MemoryStream imageStream)
        {
            _image = EncodeImage(imageStream);
            return this;
        }

        public IConfiguredFilterExecutor WithParameter(int parameter)
        {
            _parameters.Add(parameter);
            return this;
        }

        public async Task<Stream> ExecuteFilter()
        {
            var result = string.Empty;
            switch (_filterType)
            {
                case FilterType.Gauss:
                    result = await _soapServiceClient.Gauss(_image, _parameters[0]);
                    break;
                case FilterType.MakeBorder:
                    result =
                        await _soapServiceClient.MakeBorder(_image, _parameters[0], _parameters[1], _parameters[2],
                            _parameters[3]);
                    break;
                case FilterType.Sharpen:
                    result = await _soapServiceClient.Sharpen(_image);
                    break;
                case FilterType.ChangeBrightness:
                    result = await _soapServiceClient.ChangeBrightness(_image, _parameters[0], _parameters[1]);
                    break;
                case FilterType.Flip:
                    result = await _soapServiceClient.Flip(_image, _parameters[0]);
                    break;
                case FilterType.Threshold:
                    result = await _soapServiceClient.Threshold(_image, _parameters[0], _parameters[1]);
                    break;
                case FilterType.Dilate:
                    result = await _soapServiceClient.Dialte(_image, _parameters[0]);
                    break;
                case FilterType.Erode:
                    result = await _soapServiceClient.Erode(_image, _parameters[0]);
                    break;
                case FilterType.GrayScale:
                    result = await _soapServiceClient.Grayscale(_image);
                    break;
            }
            var resultDecoded = DecodeImage(result);
            return resultDecoded;
        }

        private string EncodeImage(MemoryStream image)
        {
            var bytes = image.ToArray();
            return Convert.ToBase64String(bytes);
        }

        private Stream DecodeImage(string image)
        {
            var bytes = Convert.FromBase64String(image);
            return new MemoryStream(bytes);
        }

        public IStandardExecutor ForFilter(FilterType filterType)
        {
            _filterType = filterType;
            return this;
        }

        public ICompositeFilterExecutor ForCompositeFilter()
        {
            return this;
        }

        public async Task<Stream> ExecuteFilter(IEnumerable<ParametrizedFilter> filters)
        {
            var filter = CompositeFilterBuilder.Build(filters);
            var result = await _soapServiceClient.CompositeFilter(_image, filter);
            return DecodeImage(result);
        }
    }
}
