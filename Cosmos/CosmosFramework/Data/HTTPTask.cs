using CosmosFramework;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.IO;

namespace CosmosCoreNet.Data
{
	public static class HTTPTask
	{
		public static async Task<MemoryStream> GetStream(string url, bool printResult = false)
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri(url);
			var response = await client.GetAsync(url);
			if (!response.IsSuccessStatusCode)
			{
				Debug.Log(response.ReasonPhrase, LogFormat.Error);
				return null;
			}
			else
			{
				if (printResult)
					Debug.Log(response.ReasonPhrase, LogFormat.Complete);
			}
			byte[] buffer = response.Content.ReadAsByteArrayAsync().Result;
			return new MemoryStream(buffer);
		}

		public static async Task<byte[]> GetData(string url, bool printResult = false)
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri(url);
			var response = await client.GetAsync(url);
			if (!response.IsSuccessStatusCode)
			{
				Debug.Log($"{response.ReasonPhrase}", LogFormat.Error);
				return null;
			}
			else
			{
				if (printResult)
					Debug.Log(response.ReasonPhrase, LogFormat.Complete);
			}
			byte[] buffer = response.Content.ReadAsByteArrayAsync().Result;
			return buffer;
		}
	}
}