using Cosmos.AI;
using CosmosFramework;
using CosmosFramework.CoreModule;
using CosmosFramework.InputModule;
using CosmosFramework.Netcode;
using System;
using Cosmos.AI.Open_AI;

namespace Opgave
{
	public class GameWorld : Game
	{
		private OpenAI openAi;

		public override void Initialize()
		{
			Screen.SetResolution(ScreenResolution.m_540p);
			openAi = new OpenAI("sk-");
		}
		public override void Start()
		{
			int[] values = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			Debug.LogTable(values[8..]);
			input = new KeyboardInput();

			Item item = new Item();	
			var product = new { item.name, item.price };	

			GameObject go = new GameObject();
			go.AddComponent<NetcodeObject>();
			sr = go.AddComponent<SpriteRenderer>();
			game = go.AddComponent<NetGame>();

			server = go.AddComponent<NetcodeServer>();
		}

		private SpriteRenderer sr;
		private NetcodeServer server;
		private NetGame game;

		public class Item
		{
			public string name;
			public int price;
		}

		private KeyboardInput input;

		public override async void Update()
		{
			if(!NetcodeHandler.IsConnected)
			{
				Debug.QuickLog($"Awaiting connection [C for client] - [H for server].");
				if (InputManager.GetKeyDown(Keys.C))
				{
					server.StartClient();
				}
				else if (InputManager.GetKeyDown(Keys.H))
				{
					server.StartServer();
				}
			}

			if (!NetcodeHandler.IsServer)
				return;

			Debug.QuickLog(input);

			if(InputManager.GetKeyDown(Keys.Enter))
			{
				if(input.Enabled)
				{
					string prompt = input.Read();
					Console.WriteLine($"Prompt: {prompt}");

					ImageResponse resp = await openAi.ImageGeneration.Request(new Cosmos.AI.Open_AI.ImageRequest(prompt, 1));
					await resp.Fetch();

					Debug.Log(resp.Url);

					//sr.Sprite = resp.Image;

					//TextResponse text = await openAi.TextCompletion.Request(new TextRequest(prompt, model: Model.Curie, maxTokens: 150));
					//Debug.Log(text.Responses[0].Format());

					//string[] urls = new string[]
					//{
					//	"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0D-Glxi9mpw5HtRNshYb_Iwx0fK_ibtFD2N3vZQo&s",
					//	"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0D-Glxi9mpw5HtRNshYb_Iwx0fK_ibtFD2N3vZQo&s",
					//	"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0D-Glxi9mpw5HtRNshYb_Iwx0fK_ibtFD2N3vZQo&s"
					//};
					//string[] urls = new string[]
					//{
					//	"hello",
					//	"there",
					//	"general",
					//};
					//sr.Sprite = await Sprite.FromUrl(url);
					//game.Change(resp.Url);

					//TextResponse response = await openAi.TextCompletion.Request(prompt, model: Cosmos.AI.Open_AI.Model.Ada, maxTokens: 100);
					//foreach(var resp in response)
					//{
					//	Debug.Log(resp.Format());
					//	Debug.Log(resp.FinishReason);
					//}
					//Debug.Log($"Model: {response.Model}");
					//Debug.Log($"{response.Usage}");
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