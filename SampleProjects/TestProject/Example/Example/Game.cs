using CosmosEngine;

namespace Example
{
	public class Game : CosmosEngine.CoreModule.Game
	{
		public override void Initialize()
		{
		}

		public override void Start()
		{
			GameObject gameObject = new GameObject("Name");
			gameObject.AddComponent<Vector2Example>();
			gameObject.Transform.Position = Vector2.Zero;
		}

		public override void Update()
		{
		}
	}
}