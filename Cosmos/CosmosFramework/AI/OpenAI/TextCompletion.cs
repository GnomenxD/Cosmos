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
		public async Task<TextResponse> Request(TextRequest request)
		{
			TextResponseContent content = await Request(ApiKey, OpenAI.UrlTextCompletion, request.Body());
			return TextResponse.Generate(content);
		}

		public async Task<TextResponse> Request(Prompt prompts, Model model = Model.Ada, int maxTokens = 10, double temperature = 0.7d, double p = 1.0d) => await Request(new TextRequest(prompts, model, maxTokens, temperature, p));

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