using CosmosEngine;
using CosmosEngine.Rendering;
using System.Collections.Generic;

namespace SpaceWar.Cat
{
	internal class Grid : Singleton<Grid>, CosmosEngine.CoreModule.IRenderWorld
	{
		private List<Cell> cells = new List<Cell>();
		private float radius = 1.0f;
		private float innerRadius;
		private float outerRadius;

		public static float InnerRadius => Instance.innerRadius;
		public static float OuterRadius => Instance.outerRadius;

		public override void Start()
		{
			innerRadius = HexMetrics.innerRadius * radius;
			outerRadius = HexMetrics.outerRadius * radius;
			CreateGrid();
		}

		public override void Update()
		{
			Vector2 worldPosition = Camera.Main.ScreenToWorld(InputManager.MousePosition);
			if (InputManager.GetMouseButtonDown(0))
			{
				TouchCell(worldPosition);
			}

			Coordinates coordinates = Coordinates.FromPosition(worldPosition);
			Debug.FastLog(coordinates);
			int index = cells.FindIndex(item => item.Coordinates == coordinates); // coordinates.X + coordinates.Y * 10 + coordinates.Y / 2;
			if (index >= 0 && index < cells.Count)
			{
				if (current != null)
					current.Hovered = false;
				Cell cell = cells[index];
				current = cell;
				current.Hovered = true;
			}
			else
			{
				if (current != null) 
					current.Hovered = false;
				current = null;
			}
		}

		private Cell current;

		private void CreateGrid()
		{
			int size = 10;
			for(int x = 0; x < size; x++)
			{
				for(int y = 0; y < size; y++)
				{
					int xPos = x - size / 2;
					int yPos = y - size / 2;
					//if ((xPos == -(size / 2) && yPos == -(size / 2)) || (xPos == (size / 2) && yPos == (size / 2)))
					//	continue;

					Cell hex = new Cell();
					Vector2 position = Vector2.Zero;
					position.X = xPos * (Grid.OuterRadius * 1.5f);
					position.Y = (yPos + xPos * 0.5f - xPos / 2) * (Grid.InnerRadius * 2f); 
					hex.Position = position;
					hex.Coordinates = Coordinates.FromOffset(xPos, yPos);

					cells.Add(hex);
				}
			}
		}

		private void TouchCell(Vector2 position)
		{
			Coordinates coordinates = Coordinates.FromPosition(position);
			int index = cells.FindIndex(item => item.Coordinates == coordinates); // coordinates.X + coordinates.Y * 10 + coordinates.Y / 2;
			if (index >= 0 && index < cells.Count)
			{
				Cell cell = cells[index];
				cell.Touch();
			}
		}

		public override void OnDrawGizmos()
		{
			foreach (Cell c in cells)
			{
				Gizmos.Colour = (c.Hovered && c.Touched) ? Colour.Orange : c.Hovered ? Colour.SkyeBlue : c.Touched ? Colour.Lime : Colour.DarkSlateRed;

				for(int i = 0; i < HexMetrics.corners.Length - 1; i++)
				{
					Vector2 pointA = HexMetrics.corners[i];
					Vector2 pointB = HexMetrics.corners[i + 1];

					Gizmos.DrawLine(c.Position + pointA * 0.95f, c.Position + pointB * 0.95f, 4);
				}
			}

			Gizmos.Colour = Colour.White;
			Gizmos.DrawCircle(Camera.Main.ScreenToWorld(InputManager.MousePosition), 0.2f);
		}

		public void Render()
		{
			foreach (Cell c in cells)
			{
				//Draw.SpriteBatch.DrawString(Fonts.Verdana[11], c.ToString(), c.World.ToXna(), Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, 1.0f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1.0f);
				Draw.Text(c.Coordinates.ToString(), Fonts.Verdana, 14, (c.Position + Vector2.Left * radius / 2.2f) * 100, Colour.White, short.MaxValue);
			}
		}
	}
}