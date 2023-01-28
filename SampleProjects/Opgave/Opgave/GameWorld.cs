using CosmosFramework;
using CosmosFramework.CoreModule;
using CosmosFramework.InputModule;
using CosmosFramework.Tweening;
using Newtonsoft.Json;
using OpenAI_API;
using Opgave.Blueprints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Tensorboard;
using System.Text;

namespace Opgave
{
	public class GameWorld : Game
	{

		private OpenAIAPI openAi;
		public override void Initialize()
		{
			openAi = new OpenAIAPI("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");
		}
		public override async void Start()
		{
			int[] values = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			Debug.LogTable(values[8..]);
			input = new KeyboardInput();

			Item item = new Item();
			var product = new { item.name, item.price };

			GameObject go = new GameObject();
			sr = go.AddComponent<SpriteRenderer>();
		}

		private SpriteRenderer sr;

		public class Item
		{
			public string name;
			public int price;
		}

		private KeyboardInput input;

		public override async void Update()
		{
			Debug.QuickLog(input);

			if(InputManager.GetKeyDown(Keys.Enter))
			{
				if(input.Enabled)
				{
					string prompt = input.Read();
					Console.WriteLine($"Prompt: {prompt}");

					string key = "";

					string response = await GenerateImage(key, new Input() { prompt = prompt, n = 1, size = "256x256" });
					Console.WriteLine($"RESPONE: {response}");
					sr.Sprite = await Sprite.FromUrl(response);
					//await Request(prompt, Model.AdaText, 0.9);
					//await Request(prompt, Model.BabbageText, 0.9);
					//await Request(prompt, Model.CurieText, 0.9);
					//await Request(prompt, Model.DavinciText, 0.9);
				}
				else
				{
					input.Begin();
				}
			}
		}

		public async Task<string> GenerateImage(string APIKEY, Input input)
		{
			// create a response object
			var resp = new ResponseModel();
			using (var client = new HttpClient())
			{
				// clear the default headers to avoid issues
				client.DefaultRequestHeaders.Clear();

				// add header authorization and use your API KEY
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKEY);

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
			return resp.data[0].url;
		}

		private async Task Request(string s, Model model, double tmp)
		{
			CompletionRequest request = new CompletionRequest(s, model: model, max_tokens: 40, temperature: tmp);
			var result = await openAi.Completions.CreateCompletionAsync(request);
			Debug.Log($"Result [{tmp:F2}, {model.ModelID}]: {result.ToString()}\n");
			Console.WriteLine(result.ToString());
		}
	}


	public class Input
	{
		public string? prompt { get; set; }
		public short? n { get; set; }
		public string? size { get; set; }
	}

	// model for the image url
	public class Link
	{
		public string? url { get; set; }
	}

	// model for the DALL E api response
	public class ResponseModel
	{
		public long created { get; set; }
		public List<Link>? data { get; set; }
	}


}