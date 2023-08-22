using CosmosFramework;
using CosmosFramework.Modules;
using CosmosFramework.Rendering;

namespace IsometicProject
{
    internal class IsometicMap : RenderModule<IsometicMap>, IUpdateModule
	{
		private static readonly Vector2 invalid = new Vector2(-1, -1);
		private Grid<Tile> grid;

		private float tileWidth;
		private float tileDepth;
		private Vector2 previousTile;

		private float HalfWidth => tileWidth / 2f;
		private float HalfHeight => tileDepth / 2f;

		public override void Initialize()
		{
			base.Initialize();
			worldSpace = WorldSpace.World;
			previousTile = invalid;
			tileWidth = Assets.LandscapeTilesGrass.Width / 100f;
			tileDepth = Assets.LandscapeTilesGrass.Height / 150f; //why are we dividing by 150 instead of 100?
		}

		public static void CreateMap(int width, int height)
		{
			Singleton.ConstructMap(new Tile[width, height]);	
		}

		private void ConstructMap(Tile[,] map)
		{
			grid = map;

			grid.For((x, y) =>
			{
				Tile tile = new Tile(x, y);
				grid[x, y] = tile;
			});

			foreach(var item in grid)
			{
				Debug.Log($"{item}");
			}
		}

		void IUpdateModule.Update()
		{
			Vector2 mouseWorld =	Camera.Main.ScreenToWorld(InputManager.MousePosition);
			Vector2 tilePosition = WorldToMap(mouseWorld);
			Debug.QuickLog($"Tile: {tilePosition}");

			if(previousTile != invalid)
			{
				grid[(int)previousTile.X, (int)previousTile.Y] += false;
			}
			if(tilePosition != invalid)
			{
				grid[(int)tilePosition.X, (int)tilePosition.Y] += true;
			}
			previousTile = tilePosition;


			foreach (var item in grid)
			{
				if (item == null)
					continue;
				Debug.Log($"{item} --- {MapToWorld(item.Point)}");
			}
		}

		private Vector2 MapToWorld(Int2 point) => MapToWorld(point.X, point.Y);
		
		private Vector2 MapToWorld(int x, int y)
		{
			float posX = (x - y) * (tileWidth / 2f);
			float posY = (x + y) * (tileDepth / 2f);
			return new Vector2(posX, posY);
		}

		private Vector2 WorldToMap(Vector2 world)
		{
			int x = Mathf.RoundToInt((world.X / HalfWidth + world.Y / HalfHeight) / 2);
			int y = Mathf.RoundToInt((world.Y / HalfHeight -(world.X / HalfWidth)) / 2);
			if (x < 0 || y < 0 || x >= grid.Length(0) || y >= grid.Length(1))
				return invalid;
			return new Vector2(x, y);
		}

		protected override void Render()
		{
			if(grid == null)
			{
				return;
			}

			for(int x = 0; x < grid.Length(0); x++)
			{
				for(int y = 0; y < grid.Length(1); y++)
				{
					Colour colour = Colour.LightGrey;
					if (grid[x,y])
					{
						colour = Colour.White;
					}
					Vector2 position = MapToWorld(x, y);
					Draw.Sprite(Assets.LandscapeTilesGrass, position, colour, (short)(x + y));
				}
			}
		}
	}
}