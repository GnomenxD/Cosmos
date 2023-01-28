namespace Cosmos.AI.Open_AI
{
	public readonly struct ImageRequest
	{
		private readonly string prompt;
		private readonly short amount;
		private readonly ImageSize size;

		internal string Prompt => prompt;
		internal short N => amount;
		internal string Size => size.Convert();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="prompt">The prompt of which the AI will use to generate an image.</param>
		/// <param name="amount">The amount of images that are generated.</param>
		/// <param name="size">The size of the image which is returned.</param>
		public ImageRequest(string prompt, short amount, ImageSize size)
		{
			this.prompt = prompt;
			this.amount = amount;
			this.size = size;
		}

		internal Input ToInput() => new Input() { prompt = Prompt, n = N, size = Size };
	}

	internal class Input
	{
		public string? prompt { get; set; }
		public short? n { get; set; }
		public string? size { get; set; }
	}
}