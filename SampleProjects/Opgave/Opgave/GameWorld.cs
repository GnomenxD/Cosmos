using Cosmos.AI;
using CosmosFramework;
using CosmosFramework.CoreModule;
using CosmosFramework.InputModule;
using System;

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

					TextResponse response = await openAi.TextCompletion.Request(prompt, model: Cosmos.AI.Open_AI.Model.Ada, maxTokens: 100);
					foreach(var resp in response)
					{
						Debug.Log(resp.Format());
						Debug.Log(resp.FinishReason);
					}
					Debug.Log($"Model: {response.Model}");
					Debug.Log($"{response.Usage}");
				}
				else
				{
					input.Begin();
				}
			}
		}

		private async void GenerateImage(string prompt)
		{
			ImageResponse response = await openAi.ImageGeneration.Request(new Cosmos.AI.Open_AI.ImageRequest(prompt, 1, Cosmos.AI.Open_AI.ImageSize.p256));
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