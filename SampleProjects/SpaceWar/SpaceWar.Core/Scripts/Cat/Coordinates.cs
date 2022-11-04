using CosmosEngine;

namespace SpaceWar.Cat
{
	internal struct Coordinates
	{
		private int x;
		private int y;

		public int X => x;
		public int Y => y;
		public int Z => -x - y;

		public Coordinates(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static Coordinates FromOffset(int x, int y)
		{
			return new Coordinates(y - x / 2, x);
		}

		public static Coordinates FromPosition(Vector2 position)
		{
			float x = position.X / (Grid.InnerRadius * 2f);
			float z = -x;
			float offset = position.Y / (Grid.OuterRadius * 3f);
			x -= offset;
			z -= offset;

			int iX = Mathf.RoundToInt(x);
			int iZ = Mathf.RoundToInt(z);
			int iY = Mathf.RoundToInt(-x - z);

			if (iX + iY + iZ != 0)
			{
				float dX = Mathf.Abs(x - iX);
				float dZ = Mathf.Abs(z - iZ);
				float dY = Mathf.Abs(-x - z - iY);

				if (dX > dZ && dX > dY)
				{
					iX = -iZ - iY;
				}
				else if (dY > dZ)
				{
					iY = -iX - iZ;
				}
			}

			return new Coordinates(iX, iY);
		}

		public override string ToString()
		{
			return $"{X}, {Y}, {Z}";
		}

		public static bool operator ==(Coordinates lhs, Coordinates rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
		public static bool operator !=(Coordinates lhs, Coordinates rhs) => !(lhs == rhs);
	}
}