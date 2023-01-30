using CosmosFramework;

namespace IsometicProject
{
	internal class Tile
	{
		private Int2 position;
		private bool highlight;

		public Int2 Point => position;
		public bool Highlight { get => highlight; set => highlight = value; }

		public Tile(Int2 position)
		{
			this.position = position;
		}

		public Tile(int x, int y)
		{
			this.position = new Int2(x, y);
		}

		public override string ToString()
		{
			return $"Tile [{position.X} : {position.Y}]";
		}

		public static Tile operator +(Tile tile, bool value)
		{
			if (tile == null)
				return default;
			tile.highlight = value;
			return tile;
		}

		public static Tile operator -(Tile tile, bool value)
		{
			if (tile == null)
				return default;
			tile.highlight = value;
			return tile;
		}

		public static implicit operator bool(Tile tile)
		{
			if (tile == null)
				return false;
			return tile.Highlight;
		}
	}
}