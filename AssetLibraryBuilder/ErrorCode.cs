namespace AssetLibraryBuilder
{
	internal static class ErrorCode
	{
		public static string ArgumentOutOfRange(object obj)
		{
			return $"\tError Code 001: Argument parameters were incorrect, make sure Build Event is correctly setup.";
		}
		public static string NoAssetDirectory(object obj)
		{
			return $"\tError Code 002: Asset directory could not be detected. Asset library could not be automatically created.";
		}
		public static string NoOutputDirectory(object obj)
		{
			return $"\tError Code 003: Output directory could not be detected, this can sometimes be solved by building the solution again.";
		}
		public static string MismatchConfigurationDirectory(object obj)
		{
			string configuration = string.Empty;
			if(obj is string s)
			{
				configuration = s;
			}
			return $"\tError Code 004: Output directory for {(string.IsNullOrWhiteSpace(configuration) ? "the" : $"{configuration}")} configuration could not be found. Make sure Cosmos Framework and the project shares the same build configruation.";
		}
		public static string AssetsFileMissing(object obj)
		{
			string path = string.Empty;
			if(obj is string s)
			{
				path = s;
			}
			return $"\tError Code 005: Assets.cs is not present in the solution, a new file will be generated{(string.IsNullOrWhiteSpace(path) ? "." : $" at {path}")}";
		}
		public static string DuplicateSprite(object obj)
		{
			string assetName = string.Empty;
			if (obj is SpriteAssetReference reference)
			{
				assetName = reference.Name;
			}
			return $"\tError Code 006: Sprite with the same name have been located, this is not supported.";
		}
		public static string MaximumSizeOverflow(object obj)
		{
			string assetName = string.Empty;
			if(obj is string s)
			{
				assetName = s;
			}
			return $"\tError Code 007: Asset {(string.IsNullOrWhiteSpace(assetName) ? "" : $"to {assetName}")} excessed maximum size. It must be reduced or imported manually.";
		}
	}
}