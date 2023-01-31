using CosmosFramework;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.AI.Open_AI
{
	public class TextCompletion : BaseTool
	{
		public TextCompletion(OpenAI ai) : base(ai)
		{
		}

		/// <summary>
		/// Creates a completion for the provided prompt and parameters
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<TextResponse> Request(TextRequest request)
		{
			Debug.Log($"Text completion request {request}");
			TextResponseContent content = await Request(ApiKey, OpenAI.UrlTextCompletion, request.ConstructBody());
			return TextResponse.Generate(content);
		}

		public async Task<TextResponse> Request(Prompt prompts, Model model = Model.Ada, string suffix = default, int maxTokens = 10, double temperature = 0.7d, double p = 1.0d, int amount = 1, bool echo = default, string stopSequence = default) => await Request(new TextRequest(prompts, model, suffix, maxTokens, temperature, p, amount, echo, stopSequence));

		private static async Task<TextResponseContent?> Request(string apiKey, string url, TextRequestBody body)
		{
			TextResponseContent resp = new TextResponseContent();
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
					resp = JsonConvert.DeserializeObject<TextResponseContent>(content);
				}
			}
			return resp;
		}
	}
}