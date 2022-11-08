using CosmosEngine.Variables;

namespace CosmosEngine
{
	public static class DefaultGeometry
	{
		private static Sprite square;
		private static Sprite circle;
		private static Sprite pixel;

		public static Sprite Square => square ??= (Sprite)new ContentSprite("DefaultGeometry/spr_square");
		public static Sprite Circle => circle ??= (Sprite)new ContentSprite("DefaultGeometry/spr_circle");
		public static Sprite Pixel => pixel ??= (Sprite)new ContentSprite("DefaultGeometry/spr_pixel");
	}
}