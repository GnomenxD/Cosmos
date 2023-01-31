using Cosmos.AI;
using CosmosFramework;
using CosmosFramework.CoreModule;
using CosmosFramework.InputModule;
using CosmosFramework.Netcode;
using System;
using Cosmos.AI.Open_AI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opgave
{
	public class GameWorld : Game
	{
		private OpenAI ai;
		private SpriteRenderer sr;
		private KeyboardInput input;

		public override void Initialize()
		{
			Screen.SetResolution(ScreenResolution.m_540p);
			ai = new OpenAI("sk-");
		}
		public override void Start()
		{
			input = new KeyboardInput();

			GameObject go = new GameObject();
			go.AddComponent<NetcodeObject>();
			sr = go.AddComponent<SpriteRenderer>();
		}

		public override async void Update()
		{
			Debug.QuickLog(input);

			if(InputManager.GetKeyDown(Keys.Enter))
			{
				if(input.Enabled)
				{
					string prompt = input.Read();
					Console.WriteLine($"Prompt: {prompt}");

					ImageResponse resp = await ai.ImageGeneration.Request(new Cosmos.AI.Open_AI.ImageRequest(prompt, 1));
					await resp.Fetch();
					sr.Sprite = resp.Image;
				}
				else
				{
					input.Begin();
				}
			}
		}

		private async Task Image(string prompt)
		{
			ImageRequest request = new ImageRequest(
				prompts: prompt,		//The prompt(s) used for image generation.
				amount: 3,				//The amount of images generated using the prompt.
				size: ImageSize.p256);	//The size of the images generated.

			//Post an image request to the AI.
			ImageResponse response = await ai.ImageGeneration.Request(request);
			
			//Convert the AI generated images into useable Textures for the game. 
			await response.Fetch();

			//Create GameObject for each of the generated images.
			Vector2 position = new Vector2(response.Images.Count * -2, 0);
			foreach(var image in response)
			{
				GameObject go = new GameObject("Image");
				go.Transform.Position = position;
				SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
				sr.Sprite = image;
			}
		}

		private async void Text(string prompt)
		{
			TextRequest request = new TextRequest(
				prompts: prompt,				//The prompt(s) to generate completion for.
				model: Model.Curie,				//The model to use.
				suffix: string.Empty,			//What that comes after the completion text.
				maxTokens: 40,					//The maximum number of tokens to generates.
				temperature: 0.7d,				//Sampling temperature "randomness"
				p: 1.0d,						//Nucleaus sampling
				amount: 1,						//The amount of completions to generate.
				echo: false,					//Echos back the prompt in addition to the completion.
				stopSequence: string.Empty);	//A sequence that stops the AI from generating further tokkens.

			//Post a text completion request to the AI.
			TextResponse response = await ai.TextCompletion.Request(request);

			//Collect all the responses in a list for later use.
			List<string> answers = new List<string>();
			foreach(var resp in response)
			{
				answers.Add(resp.Response);
			}
		}
	}


}