using CosmosEngine;

namespace SpaceWar.Cat
{
	internal class Cell
	{
		private Vector2 position;
		private Coordinates coordinates;
		private bool touched;
		private bool hovered;

		internal Vector2 Position { get => position; set => position = value; }
		internal Coordinates Coordinates { get => coordinates; set => coordinates = value; }
		internal bool Touched { get => touched; set => touched = value; }
		internal bool Hovered { get => hovered; set => hovered = value; }

		public void Touch()
		{
			touched = !touched;
		}
	}
}