using CosmosFramework;
using CosmosFramework.CoreModule;

namespace Opgave
{
	public class GameWorld : Game
	{
		private Flag data;

		public override void Initialize()
		{
			data = new Flag();
		}
		public override void Start()
		{
			GameObject gameObject = new GameObject("My Object");
			SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
			sr.Sprite = Assets.PlayerShip1Green;
		}

		public override void Update()
		{
			Debug.QuickLog($"ID: {data.ToString()} | {data.ToByteString()}");

			switch (InputManager.GetCurrentKey())
			{
				case CosmosFramework.InputModule.Keys.D1:
					break;
			}

			if (InputManager.GetKeyDown(CosmosFramework.InputModule.Keys.Space))
			{
				Debug.LogTable(data.Read());
				data.Clear();
			}
		}
	}
}