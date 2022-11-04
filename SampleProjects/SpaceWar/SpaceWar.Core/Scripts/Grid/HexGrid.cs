using CosmosEngine;
using CosmosEngine.CoreModule;
using CosmosEngine.Rendering;
using System.Collections.Generic;

namespace SpaceWar
{
	public class HexGrid : Singleton<HexGrid>, IRenderWorld
	{
		private readonly List<HexCell> cells = new List<HexCell>();
		private float radius = 1f;
		private float lineThickness;
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
			if(InputManager.GetMouseButton(0))
			{
				Vector2 world = Camera.Main.ScreenToWorld(InputManager.MousePosition);
				CellFromWorld(world);
			}
		}

		public void CreateGrid()
		{
			int size = 2;
			for(int x = -size; x <= size; x++)
			{
				for(int y = -size; y <= size; y++)
				{
					//if (x == -1 && y == -1 || x == 1 && y == 1)
					//	continue;
					AddTile(x, y);
				}
			}
		}

		public void AddTile(int q, int r)
		{
			HexCell cell = new HexCell(q, r);
			cells.Add(cell);
		}

		public void CellFromWorld(Vector2 worldPosition)
		{
			//float q = (2f / 3 * worldPosition.X) / HexGrid.OuterRadius;
			//float r = (-1f / 3 * worldPosition.X + Mathf.Sqrt(3)/3f * worldPosition.Y) / HexGrid.OuterRadius;
			//Debug.FastLog($"q: {q}, r: {r} --- q: {Mathf.Round(q)}, r: {Mathf.Round(r)}");

			//return;
			//worldPosition -= new Vector2(0.5f, 0.5f);
			Debug.FastLog($"World: {worldPosition}");
			// Find out which major row and column we are on:
			int row = (int)(worldPosition.Y / (HexGrid.OuterRadius * HexMetrics.sin60));
			int column = (int)(worldPosition.X / (HexGrid.OuterRadius * 1.5f));

			// Compute the offset into these row and column:
			float dy = worldPosition.Y - row * HexGrid.OuterRadius * HexMetrics.sin60;
			float dx = worldPosition.X - column * HexGrid.OuterRadius * 1.5f;

			Debug.FastLog($"row: {row}, column: {column}, dy: {dy}, dx: {dx}");

			// Are we on the left of the hexagon edge, or on the right?
			if (((row ^ column) & 1) == 0)
			{
				dy = HexMetrics.sin60 - dy;
			}

			int right = dy * HexGrid.OuterRadius * 0.5f < HexMetrics.sin60 * (dx - HexGrid.OuterRadius * 0.5f) ? 1 : 0;

			// Now we have all the information we need, just fine-tune row and column.
			row += (column ^ row ^ right) & 1;
			column += right;
			//int q = column;
			//int r = (row / 2) - q / 2;
			Debug.FastLog($"Column {column} Row {row / 2} Right {right}");
			//Debug.FastLog($"Q {q} R {r} ");
			GridCoordinate coordinate = new GridCoordinate(column, row / 2);
			Debug.FastLog(coordinate);
		}

		public static void DrawHexagon(Vector2 world, float radius, bool touched = false)
		{
			float penalty = (radius / 10f);
			float scale = 0.9f;
			Gizmos.Colour = touched ? Colour.Cyan : Colour.DarkSlateRed;
			Gizmos.DrawCircle(world, radius * 0.5f);

			Gizmos.Colour = Colour.DarkGreen;
			Gizmos.DrawRay(world, Vector2.Up * HexMetrics.innerRadius * radius * scale, 6);
			Gizmos.Colour = Colour.Green;
			Gizmos.DrawRay(world, Vector2.Up * HexMetrics.innerRadius * radius);

			Gizmos.Colour = Colour.DarkRed;
			Gizmos.DrawRay(world, Vector2.Right * HexMetrics.outerRadius * radius * scale, 6);
			Gizmos.Colour = Colour.Red;
			Gizmos.DrawRay(world, Vector2.Right * HexMetrics.outerRadius * radius);

			for (int i = 0; i < HexMetrics.corners.Length - 1; i++)
			{
				Vector2 pointA = HexMetrics.corners[i] * radius;
				Vector2 pointB = HexMetrics.corners[i + 1] * radius;
				Vector2 direction = (pointA - pointB).Normalized;
				Vector2 perpendicular = Vector2.Perpendicular(direction) * penalty * 0.5f;

				Gizmos.Colour = Colour.Violet;
				Gizmos.DrawLine(world + (pointA - direction * (penalty / 10f) - perpendicular) * scale, world + (pointB + direction * (penalty / 10f) - perpendicular) * scale, 10);

				Gizmos.Colour = Colour.LightGrey;
				Gizmos.DrawLine(world + pointA, world + pointB, 1);
			}
		}

		public override void OnDrawGizmos()
		{
			foreach (HexCell cell in cells)
			{
				DrawHexagon(cell.World, radius, cell.Touched);
			}
			//Vector2 one = HexMetrics.corners[0] * radius;
			//Vector2 two = HexMetrics.corners[1] * radius;

			//float penalty = radius / thickness;
			//for (int i = 0; i < HexMetrics.corners.Length - 1; i++)
			//{
			//	Vector2 pointA = HexMetrics.corners[i] * radius;
			//	Vector2 pointB = HexMetrics.corners[i + 1] * radius;
			//	Vector2 direction = (pointA - pointB).Normalized;
			//	Vector2 perpendicular = Vector2.Perpendicular(direction) * penalty;
			//	Gizmos.DrawLine(pointA - perpendicular, pointB - perpendicular, thickness);
			//}

			//Gizmos.Colour = Colour.Lime;
			//Gizmos.DrawRay(Vector2.Zero, Vector2.Up * HexMetrics.innerRadius * radius);
			//Gizmos.Colour = Colour.Red;
			//Gizmos.DrawRay(Vector2.Zero, Vector2.Right * HexMetrics.outerRadius * radius);
			//Gizmos.Colour = Colour.SkyeBlue;
			//Gizmos.DrawRay(one, (two - one).Normalized * radius);
			//Gizmos.Colour = Colour.DesaturatedRed;
			//Gizmos.DrawRay(one, Vector2.Perpendicular((two - one).Normalized));
		}

		public void Render()
		{
			foreach (HexCell c in cells)
			{
				//Draw.SpriteBatch.DrawString(Fonts.Verdana[11], c.ToString(), c.World.ToXna(), Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, 1.0f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1.0f);
				Draw.Text(c.ToString(), Fonts.Verdana, 11, c.World * 100, Colour.White, short.MaxValue);
			}
		}
	}
}