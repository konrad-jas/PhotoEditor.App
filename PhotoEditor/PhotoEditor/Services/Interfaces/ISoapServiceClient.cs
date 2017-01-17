using System.Threading.Tasks;

namespace PhotoEditor.Services.Interfaces
{
	public interface ISoapServiceClient
	{
		Task<string> GetWeather(string city, string country);
	}
}