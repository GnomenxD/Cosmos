using CosmosEngine;

namespace SpaceWar
{
	internal struct GridCoordinate
	{
		private int x;
		private int y;

		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }
		public int Z => -x - y;

		public GridCoordinate(int x, int y)
		{
			this.x = x;
			this.y = y - x / 2;
		}

		public static GridCoordinate FromWorld(Vector2 point)
		{
			return default(GridCoordinate);
		}

		public override string ToString()
		{
			return $"x: {x}, y: {y}, z: {Z}";
		}
	}
}