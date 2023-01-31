using Cosmos.AI.Open_AI;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cosmos.AI
{
	public class OpenAI
	{
		internal const string UrlModels = "https://api.openai.com/v1/models)";
		internal const string UrlTextCompletion = "https://api.openai.com/v1/completions";
		internal const string UrlImageGeneration = "https://api.openai.com/v1/images/generations";

		private readonly string apiKey;
		private readonly ImageGenerator imageGeneration;
		private readonly TextCompletion textCompletion;

		internal string ApiKey => apiKey;
		public ImageGenerator ImageGeneration => imageGeneration;
		public TextCompletion TextCompletion => textCompletion;

		/// <summary>
		/// OpenAI should be constructed with your API key, find it here: <see href="https://platform.openai.com/account/api-keys">Account/api-keys</see>.
		/// <para>API key should not be uploaded to github or any other platform.</para>
		/// </summary>
		/// <param name="apiKey"></param>
		public OpenAI(string apiKey)
		{
			this.apiKey = apiKey;
			imageGeneration = new ImageGenerator(this);
			textCompletion = new TextCompletion(this);
		}

		public async Task<string> ListModels()
		{
			var resp = string.Empty;
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Clear();

				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

				var Message = await client.GetAsync(UrlModels);

				resp = await Message.Content.ReadAsStringAsync();
			}
			return resp;
		}
	}
}