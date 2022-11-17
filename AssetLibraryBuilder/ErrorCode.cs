namespace AssetLibraryBuilder
{
	internal static class ErrorCode
	{
		public static string ArgumentOutOfRange(object obj)
		{
			return $"\tError Code 0001: Argument parameters were incorrect, make sure Build Event is correctly setup.";
		}
		public static string NoAssetDirectory(object obj)
		{
			return $"\tError Code 0002: Asset directory could not be detected. Asset library could not be automatically created.";
		}
		public static string NoOutputDirectory(object obj)
		{
			return $"\tError Code 0003: Output directory could not be detected, this can sometimes be solved by building the solution again.";
		}
		public static string AssetsFileMissing(object obj)
		{
			string path = string.Empty;
			if(obj is string s)
			{
				path = s;
			}
			return $"Error Code 0004: Assets.cs is not present in the solution, a new file will be generated{(string.IsNullOrWhiteSpace(path) ? "." : $" at {path}")}";
		}
		public static string DuplicateSprite(object obj)
		{
			string assetName = string.Empty;
			if (obj is SpriteAssetReference reference)
			{
				assetName = reference.Name;
			}
			return $"Error Code 0005: Sprite with the same name have been located, this is not supported.";
		}
		public static string MaximumSizeOverflow(object obj)
		{
			string assetName = string.Empty;
			if(obj is string s)
			{
				assetName = s;
			}
			return $"\tError Code 0006: Asset {(string.IsNullOrWhiteSpace(assetName) ? "" : $"to {assetName}")} excessed maximum size. It must be reduced or imported manually.";
		}
	}
}