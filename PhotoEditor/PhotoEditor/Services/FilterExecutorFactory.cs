using System;
using System.IO;
using System.Threading.Tasks;
using PhotoEditor.Services.Interfaces;
using PhotoEditor.Utility;

namespace PhotoEditor.Services
{
    public class FilterExecutorFactory : IFilterExecutorFactory
    {
        private readonly ISoapServiceClient _soapServiceClient;
        public FilterExecutorFactory(ISoapServiceClient soapServiceClient)
        {
            _soapServiceClient = soapServiceClient;
        }

        public IFilterExecutor GetExecutor()
        {
            return new FilterExecutor(_soapServiceClient);
        }
    }
}