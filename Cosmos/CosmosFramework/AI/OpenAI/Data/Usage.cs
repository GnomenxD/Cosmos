namespace Cosmos.AI.Open_AI
{
	public class Usage
	{
		public int prompt_tokens { get; set; }
		public int completion_tokens { get; set; }
		public int total_tokens { get; set; }

		public override string ToString()
		{
			return $"Tokens used [Prompt: {prompt_tokens}] [Completion: {completion_tokens}] [Total: {total_tokens}]";
		}
	}
}