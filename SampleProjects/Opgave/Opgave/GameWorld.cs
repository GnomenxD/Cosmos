using CosmosFramework;
using CosmosFramework.CoreModule;
using CosmosFramework.InputModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Cosmos.AI;

namespace Opgave
{
	public class GameWorld : Game
	{
		private OpenAI openAi;

		public override void Initialize()
		{
			openAi = new OpenAI("sk-");
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

					Debug.TimeLog(GenerateImage, prompt);



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

		private async void GenerateImage(string prompt)
		{
			Response response = await openAi.ImageGeneration.Request(new Cosmos.AI.Open_AI.ImageRequest(prompt, 1, Cosmos.AI.Open_AI.ImageSize.p256));
			sr.Sprite = response.Image;
		}

		//private async Task Request(string s, Model model, double tmp)
		//{
		//	CompletionRequest request = new CompletionRequest(s, model: model, max_tokens: 40, temperature: tmp);
		//	var result = await openAi.Completions.CreateCompletionAsync(request);
		//	Debug.Log($"Result [{tmp:F2}, {model.ModelID}]: {result.ToString()}\n");
		//	Console.WriteLine(result.ToString());
		//}
	}


}