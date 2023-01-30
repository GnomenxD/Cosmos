namespace Cosmos.AI.Open_AI
{
	public readonly struct ImageRequest
	{
		private readonly string prompt;
		private readonly short amount;
		private readonly ImageSize size;

		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.ImageRequestBody.prompt"/>
		/// </summary>
		internal string Prompt => prompt;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.ImageRequestBody.n"/>
		/// </summary>
		internal short N => amount;
		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.ImageRequestBody.size"/>
		/// </summary>
		internal string Size => size.Convert();

		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.ImageRequestBody"/>
		/// </summary>
		/// <param name="prompt"><inheritdoc cref="Cosmos.AI.Open_AI.ImageRequestBody.prompt"/></param>
		/// <param name="amount"><inheritdoc cref="Cosmos.AI.Open_AI.ImageRequestBody.n"/></param>
		/// <param name="size"><inheritdoc cref="Cosmos.AI.Open_AI.ImageRequestBody.size"/></param>
		public ImageRequest(string prompt, short amount = 1, ImageSize size = ImageSize.p256)
		{
			this.prompt = prompt;
			this.amount = amount;
			this.size = size;
		}

		/// <summary>
		/// Converts <see cref="Cosmos.AI.Open_AI.ImageRequest"/> into <see cref="Cosmos.AI.Open_AI.ImageRequestBody"/>.
		/// </summary>
		/// <returns></returns>
		internal ImageRequestBody Body() => new ImageRequestBody() 
		{ 
			prompt = Prompt, 
			n = N, 
			size = Size
		};
	}

	/// <summary>
	/// Creates an image given a prompt.
	/// </summary>
	internal class ImageRequestBody
	{
		/// <summary>
		/// A text description of the desired image(s). The maximum length is 1000 characters.
		/// </summary>
		public string? prompt { get; set; }
		/// <summary>
		/// The number of images to generate. Must be between 1 and 10.
		/// </summary>
		public short? n { get; set; }
		/// <summary>
		/// The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024.
		/// </summary>
		public string? size { get; set; }
	}
}