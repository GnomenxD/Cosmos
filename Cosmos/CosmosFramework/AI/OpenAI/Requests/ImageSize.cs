namespace Cosmos.AI.Open_AI
{
	public enum ImageSize
	{
		/// <summary>
		/// 256x256
		/// </summary>
		p256,
		/// <summary>
		/// 512x512
		/// </summary>
		p512,
		/// <summary>
		/// 1024x1025
		/// </summary>
		p1024,
	}

	internal static class ImageSizeExtension
	{
		public static string Convert(this ImageSize imageSize) => imageSize switch
		{
			ImageSize.p256 => "256x256",
			ImageSize.p512 => "512x512",
			ImageSize.p1024 => "1024x1024",
			_ => "256x256",
		};

		public static ImageSize Convert(this string imageSize) => imageSize switch
		{
			"256x256" => ImageSize.p256,
			"512x512" => ImageSize.p512,
			"1024x1024" => ImageSize.p1024,
			_ => ImageSize.p256
		};

		public static bool IsValid(this string imageSize) => imageSize switch
		{
			"256x256" => true,
			"512x512" => true,
			"1024x1024" => true,
			_ => false
		};
	}
}