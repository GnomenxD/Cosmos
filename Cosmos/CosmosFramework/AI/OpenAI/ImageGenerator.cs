using CosmosFramework.InputModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.AI.Open_AI
{
	public class ImageGenerator : BaseTool
	{
		public ImageGenerator(OpenAI ai) : base(ai)
		{
		}

		public async Task<ImageResponse> Request(ImageRequest request)
		{
			ImageResponseContent resp = await Request(ApiKey, OpenAI.UrlImageGeneration, request.Body());
			return ImageResponse.Generate(resp);
		}

		public async Task<ImageResponse> Request(string? prompt = default, short? amount = default, string size = default) => await Request(new ImageRequest(prompt, amount.GetValueOrDefault(), size.Convert()));

		private static async Task<ImageResponseContent> Request(string apiKey, string url, ImageRequestBody body)
		{
			// create a response object
			ImageResponseContent resp = new ImageResponseContent();
			using (HttpClient client = new HttpClient())
			{
				// clear the default headers to avoid issues
				client.DefaultRequestHeaders.Clear();

				// add header authorization and use your API KEY
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

				//  call the  api using post method and set the content type to application/json
				HttpResponseMessage message = await client.PostAsync(
					url,
					new StringContent(JsonConvert.SerializeObject(body),
					Encoding.UTF8, "application/json"));

				// if result OK
				// read the content and deserialize it using the Response Model
				// then return the response object
				if (message.IsSuccessStatusCode)
				{

					string content = await message.Content.ReadAsStringAsync();
					resp = JsonConvert.DeserializeObject<ImageResponseContent>(content);
				}
			}
			return resp;
		}
	}
}