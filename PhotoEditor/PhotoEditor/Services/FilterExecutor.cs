using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;

namespace PhotoEditor.Services
{
    public class FilterExecutor : IFilterExecutor, IConfiguredFilterExecutor
    {
        private readonly ISoapServiceClient _soapServiceClient;
        private readonly List<int> _parameters;
        private MemoryStream _image;
        private FilterType _filterType;

        public FilterExecutor(ISoapServiceClient soapServiceClient)
        {
            _soapServiceClient = soapServiceClient;
            _parameters = new List<int>();
        }

        public IConfiguredFilterExecutor Configure(MemoryStream imageStream, FilterType filterType)
        {
            _image = imageStream;
            _filterType = filterType;
            return this;
        }

        public IConfiguredFilterExecutor AddParameter(int parameter)
        {
            _parameters.Add(parameter);
            return this;
        }

        public async Task<Stream> ExecuteFilter()
        {
            var imageEncoded = EncodeImage(_image);
            var result = string.Empty;
            switch (_filterType)
            {
                case FilterType.Gauss:
                    result = await _soapServiceClient.Gauss(imageEncoded, _parameters[0]);
                    break;
                case FilterType.MakeBorder:
                    result =
                        await _soapServiceClient.MakeBorder(imageEncoded, _parameters[0], _parameters[1], _parameters[2],
                            _parameters[3]);
                    break;
                case FilterType.Sharpen:
                    result = await _soapServiceClient.Sharpen(imageEncoded);
                    break;
                case FilterType.ChangeBrightness:
                    result = await _soapServiceClient.ChangeBrightness(imageEncoded, _parameters[0], _parameters[1]);
                    break;
                case FilterType.Flip:
                    result = await _soapServiceClient.Flip(imageEncoded, _parameters[0]);
                    break;
                case FilterType.Threshold:
                    result = await _soapServiceClient.Threshold(imageEncoded, _parameters[0], _parameters[1]);
                    break;
                case FilterType.Dilate:
                    result = await _soapServiceClient.Dialte(imageEncoded, _parameters[0]);
                    break;
                case FilterType.Erode:
                    result = await _soapServiceClient.Erode(imageEncoded, _parameters[0]);
                    break;
                case FilterType.GrayScale:
                    result = await _soapServiceClient.Grayscale(imageEncoded);
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
    }
}
