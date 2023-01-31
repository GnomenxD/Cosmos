using Newtonsoft.Json;

namespace Cosmos.AI.Open_AI
{
	public readonly struct ImageRequest
	{
		private readonly Prompt prompts;
		private readonly short amount;
		private readonly ImageSize size;

		/// <summary>
		/// <inheritdoc cref="Cosmos.AI.Open_AI.ImageRequestBody.prompt"/>
		/// </summary>
		internal Prompt Prompts => prompts;
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
		/// <param name="prompts">A text description of the desired image(s). The maximum length is 1000 characters.</param>
		/// <param name="amount">The number of images to generate. Must be between 1 and 10.</param>
		/// <param name="size">The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024.</param>
		public ImageRequest(Prompt prompts, short amount = 1, ImageSize size = ImageSize.p256)
		{
			this.prompts = prompts;
			this.amount = amount;
			this.size = size;
		}

		/// <summary>
		/// Converts <see cref="Cosmos.AI.Open_AI.ImageRequest"/> into <see cref="Cosmos.AI.Open_AI.ImageRequestBody"/>.
		/// </summary>
		/// <returns></returns>
		internal ImageRequestBody ConstructBody() => new ImageRequestBody() 
		{ 
			prompt = (string)Prompts, 
			n = N, 
			size = Size
		};

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this.ConstructBody());
		}
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