namespace Cosmos.AI.Open_AI
{
	public abstract class BaseTool
	{
		private readonly OpenAI ai;
		protected string ApiKey => ai.ApiKey;
		public BaseTool(OpenAI ai)
		{
			this.ai = ai;
		}
	}
}