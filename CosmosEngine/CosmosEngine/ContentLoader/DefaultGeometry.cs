namespace CosmosEngine
{
	public static class DefaultGeometry
	{
		private static Sprite square;
		private static Sprite circle;
		private static Sprite pixel;

		public static Sprite Square => square ??= new Sprite("Assets/DefaultGeometry/spr_square.png");
		public static Sprite Circle => circle ??= new Sprite("Assets/DefaultGeometry/spr_circle.png");
		public static Sprite Pixel => pixel ??= new Sprite("Assets/DefaultGeometry/spr_pixel.png");
	}
}