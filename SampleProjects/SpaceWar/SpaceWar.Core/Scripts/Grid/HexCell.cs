using CosmosEngine;

namespace SpaceWar
{
	internal class HexCell
	{
		private int q;
		private int r;
		private float radius;
		private Vector2 worldPosition;
		private bool touched;

		/// <summary>
		/// Column
		/// </summary>
		public int Q => q;
		/// <summary>
		/// Row
		/// </summary>
		public int R => r;
		/// <summary>
		/// Slice
		/// </summary>
		public int S => -q - r;
		public Vector2 World => worldPosition;
		public bool Touched => touched;

		public HexCell(int x, int y)
		{
			this.q = x;
			this.r = y;
			worldPosition = new Vector2(
				x: Q * HexGrid.OuterRadius * 1.5f, 
				y: (y + x * 0.5f - x / 2) * HexGrid.InnerRadius * 2f);
		}

		public void TouchCell() => touched = !touched;

		public override string ToString()
		{
			return $"{Q}, {R}";
		}
	}
}