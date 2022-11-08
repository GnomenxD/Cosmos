using CosmosEngine.Variables;

namespace CosmosEngine.Editor
{
	public static class EditorContent
	{
		private static Sprite logMessage;
		private static Sprite logComplete;
		private static Sprite logWarning;
		private static Sprite logError;

		public static Sprite LogMessage => logMessage ??= (Sprite)new ContentSprite("Editor/log_alert");
		public static Sprite LogComplete => logComplete ??= (Sprite)new ContentSprite("Editor/log_complete");
		public static Sprite LogWarning => logWarning ??= (Sprite)new ContentSprite("Editor/log_warning");
		public static Sprite LogError => logError ??= (Sprite)new ContentSprite("Editor/log_error");
	}
}