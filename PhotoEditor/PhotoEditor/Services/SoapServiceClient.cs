//using System;
//using System.ComponentModel;
//using System.Threading.Tasks;
//using PhotoEditor.Services;
//using PhotoEditor.Services.Interfaces;

//namespace PhotoEditor.Core.Services.Implementation
//{
//	public class SoapServiceClient : AsyncServiceBase, ISoapServiceClient
//	{
//		private readonly GlobalWeatherSoapClient _client;

//		public SoapServiceClient()
//		{
//			_client = new GlobalWeatherSoapClient();
//		}

//		public async Task<string> GetWeather(string city, string country)
//		{
//			var completionSource = CreateCompletionSource<string>();
//			var handler = new EventHandler<GetWeatherCompletedEventArgs>(
//				(sender, args) => { CompleteTask(completionSource, args, () => args.Result); });
//			_client.GetWeatherCompleted += handler;
//			_client.GetWeatherAsync(city, country);
//			return await CreateContinuationTask(completionSource.Task, () => _client.GetWeatherCompleted -= handler);
//		}
//	}
//}