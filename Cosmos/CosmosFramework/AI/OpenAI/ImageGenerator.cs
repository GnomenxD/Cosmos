using CosmosFramework.InputModule;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.AI.Open_AI
{
	public class ImageGenerator
	{
		private readonly OpenAI ai;
		public ImageGenerator(OpenAI ai)
		{
			this.ai = ai;
		}

		public async Task<Response> Request(ImageRequest request)
		{
			ResponseModel resp = await Request(ai.ApiKey, request.ToInput());
			return await Response.Generate(resp);
		}

		public async Task<Response> Request(string? prompt = default, short? amount = default, string size = default) => await Request(new ImageRequest(prompt, amount.GetValueOrDefault(), size.Convert()));

		private static async Task<ResponseModel> Request(string apiKey, Input input)
		{
			// create a response object
			var resp = new ResponseModel();
			using (var client = new HttpClient())
			{
				// clear the default headers to avoid issues
				client.DefaultRequestHeaders.Clear();

				// add header authorization and use your API KEY
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

				//  call the  api using post method and set the content type to application/json
				var Message = await client.PostAsync("https://api.openai.com/v1/images/generations",
					new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json"));

				// if result OK
				// read the content and deserialize it using the Response Model
				// then return the response object
				if (Message.IsSuccessStatusCode)
				{

					var content = await Message.Content.ReadAsStringAsync();
					resp = JsonConvert.DeserializeObject<ResponseModel>(content);
				}
			}
			return resp;
		}
	}
}