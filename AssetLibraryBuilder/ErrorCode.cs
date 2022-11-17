namespace AssetLibraryBuilder
{
	internal static class ErrorCode
	{
		public static string DuplicateSprite(object obj)
		{
			if(obj is SpriteAssetReference reference)
			{

			}
			return $"Error Code 0007: Sprite with the same name have been located, this is not supported.";
		}

		public static string NoOutputDirectory(object obj)
		{
			return $"\tError Code 0003: Output directory could not be detected, this can sometimes be solved by building the solution again.";
		}
	}
}