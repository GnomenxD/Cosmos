using CosmosEngine;
using CosmosEngine.CoreModule;
using CosmosEngine.Rendering;
using System.Collections.Generic;

namespace SpaceWar.Axial
{
	internal class AxialGrid : Singleton<AxialGrid>, IRenderWorld
	{
		private static float cellRadius = 2.0f;
		private int thickness = 20;
		private readonly List<HexCell> cells = new List<HexCell>();

		public static float Radius => cellRadius;
		public void Initialize(float radius)
		{
			cellRadius = radius;
			CreateGrid();
		}

		public void CreateGrid()
		{
			AddTileToGrid(0, 0, 0);
			AddTileToGrid(0, 1, -1);
			AddTileToGrid(1, -1, 0);
			AddTileToGrid(-1, 0, 1);
		}

		public override void Update()
		{
			Debug.FastLog($"Cells: {cells.Count}");
		}

		public void AddTileToGrid(int q, int s, int r)
		{
			Vector2Int coord = HexCell.CubeToAxial(new CubeCoordinates(q, s, r));
			HexCell cell = new HexCell(coord.X, coord.Y);
			cells.Add(cell);
		}

		public override void OnDrawGizmos()
		{
			foreach (HexCell cell in cells)
			{
				Gizmos.Colour = Colour.Blue;
				Gizmos.DrawBox(cell.WorldPosition, Vector2.One * 0.5f);
				Gizmos.Colour = Colour.Green;
				Gizmos.DrawRay(cell.WorldPosition, Vector2.Up * HexMetrics.innerRadius);
				Gizmos.Colour = Colour.Red;
				Gizmos.DrawRay(cell.WorldPosition, Vector2.Right * HexMetrics.outerRadius);
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
				Draw.Text(c.ToString(), Fonts.Verdana, 10, c.WorldPosition * 100, Colour.White, short.MaxValue);
			}
		}

		public class HexCell
		{
			private int q = 0;
			private int r = 0;
			private Vector2 worldPosition;

			/// <summary>
			/// Column
			/// </summary>
			public int Q => q;
			/// <summary>
			/// Slice
			/// </summary>
			public int S => -q - r;
			/// <summary>
			/// Row
			/// </summary>
			public int R => r;
			public Vector2 WorldPosition => worldPosition;

			public HexCell(int q, int r)
			{
				this.q = q;
				this.r = r;
				this.worldPosition = AxialToWorld(this);
			}

			public override string ToString()
			{
				return $"{Q}, {S}, {R}";
			}

			public static Vector2Int CubeToAxial(CubeCoordinates cube)
			{
				int q = cube.Q;
				int r = cube.R;
				return new Vector2Int(q, r);
			}

			public static CubeCoordinates AxialToCube(Vector2Int axial)
			{
				int q = axial.X;
				int s = axial.Y;
				int r = -q - s;
				return new CubeCoordinates(q, s, r);
			}

			public static Vector2 AxialToWorld(HexCell hex)
			{
				float column = (float)(hex.R + (hex.Q + (hex.Q % 2)) / 2f);
				float row = (float)(hex.Q);
				Debug.Log($"AxialToWorld: {row}, {column}");
				return new Vector2(column, row);
			}

			public static Vector2Int WorldToAxial(Vector2 world)
			{
				int q = Mathf.RoundToInt(world.X);
				int r = Mathf.RoundToInt(world.Y - (world.X + (world.X % 2)) / 2f);
				return new Vector2Int(q, r);
			}
		}

		public struct CubeCoordinates
		{
			private int q, s, r;
			public int Q => q;
			public int S => s;
			public int R => r;
			public CubeCoordinates(int q, int s, int r)
			{
				this.q = q;
				this.s = s;
				this.r = r;
			}

			public override string ToString()
			{
				return $"({Q}, {S}, {R})";
			}
		}
	}
}