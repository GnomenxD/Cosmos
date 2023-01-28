using Cosmos.AI.Open_AI;

namespace Cosmos.AI
{
	public class OpenAI
	{
		private readonly string apiKey;

		private readonly ImageGenerator imageGeneration;

		internal string ApiKey => apiKey;
		public ImageGenerator ImageGeneration => imageGeneration;

		public OpenAI(string apiKey)
		{
			this.apiKey = apiKey;
			imageGeneration = new ImageGenerator(this);
		}
	}
}