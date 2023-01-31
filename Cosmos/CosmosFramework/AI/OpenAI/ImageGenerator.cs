using CosmosFramework.InputModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CosmosFramework;

namespace Cosmos.AI.Open_AI
{
	public class ImageGenerator : BaseTool
	{
		public ImageGenerator(OpenAI ai) : base(ai)
		{
		}

		/// <summary>
		/// The image generations endpoint allows you to create an original image given a text prompt. Generated images can have a size of 256x256, 512x512, or 1024x1024 pixels. Smaller sizes are faster to generate.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<ImageResponse> Request(ImageRequest request)
		{
			Debug.Log($"Image generation request {request}");
			ImageResponseContent resp = await Request(ApiKey, OpenAI.UrlImageGeneration, request.ConstructBody());
			return ImageResponse.Generate(resp);
		}

		public async Task<ImageResponse> Request(string? prompt = default, short? amount = default, string size = default) => await Request(new ImageRequest(prompt, amount.GetValueOrDefault(), size.Convert()));

		private static async Task<ImageResponseContent> Request(string apiKey, string url, ImageRequestBody body)
		{
			ImageResponseContent resp = new ImageResponseContent();
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Clear();

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

				HttpResponseMessage message = await client.PostAsync(
					url,
					new StringContent(JsonConvert.SerializeObject(body),
					Encoding.UTF8, "application/json"));


				Debug.Log($"{(int)message.StatusCode} - {message.ReasonPhrase}", message.IsSuccessStatusCode ? LogFormat.Complete : LogFormat.Warning);
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