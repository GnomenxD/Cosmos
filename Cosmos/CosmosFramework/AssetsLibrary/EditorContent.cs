namespace CosmosFramework.Editor
{
	public static class EditorContent
	{
		private static Sprite logMessage;
		private static Sprite logComplete;
		private static Sprite logWarning;
		private static Sprite logError;

		public static Sprite LogMessage => logMessage ??= new Sprite("Assets/Editor/log_alert.png");
		public static Sprite LogComplete => logComplete ??= new Sprite("Assets/Editor/log_complete.png");
		public static Sprite LogWarning => logWarning ??= new Sprite("Assets/Editor/log_warning.png");
		public static Sprite LogError => logError ??= new Sprite("Assets/Editor/log_error.png");
	}
}