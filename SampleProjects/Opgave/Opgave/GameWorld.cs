using CosmosFramework;
using CosmosFramework.CoreModule;

namespace Opgave
{
	public class GameWorld : Game
	{
		ulong _id;

		public override void Initialize()
		{

		}
		public override void Start()
		{
			GameObject gameObject = new GameObject("My Object");
			SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
			sr.Sprite = Assets.PlayerShip1Green;

			_id += 1;
			_id += 2;
			_id += 4;
			_id += 8;
		}

		public override void Update()
		{
			switch(InputManager.GetCurrentKey())
			{
				case CosmosFramework.InputModule.Keys.D1:
					break;
			}

			Debug.QuickLog($"ID: {_id}");
			if(InputManager.GetKeyDown(CosmosFramework.InputModule.Keys.D1))
			{

			}
			else if (InputManager.GetKeyDown(CosmosFramework.InputModule.Keys.D2))
			{

			}
			else if (InputManager.GetKeyDown(CosmosFramework.InputModule.Keys.D3))
			{

			}
		}
	}
}