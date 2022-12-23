using CosmosFramework;
using CosmosFramework.CoreModule;
using CosmosFramework.Tweening;
using Opgave.Prefabs;

namespace Opgave
{
	public class GameWorld : Game
	{
		private Flag data;
		private PlayerShip ship;
		private float value;


		public override void Initialize()
		{
			data = new Flag();
		}
		public override void Start()
		{
			EnemyShipBlueprint.Instantiate(new Vector2(5.0f, 3.0f), 90.0f, 100);
			ship = PlayerShipBlueprint.Instantiate(Vector2.Zero, 0.0f, 100, 5, 10);
			EnemyShipBlueprint.Instantiate(new Vector2(-5.0f, 3.0f), 270.0f, 150);
			value = 1.0f;

			NetworkPlayerBLueprint.Instantiate();
		}

		public override void Update()
		{
			Debug.QuickLog($"Value: {value}");
			if (InputManager.GetKeyDown(CosmosFramework.InputModule.Keys.Space))
			{
				Debug.Log("Start Tween");
				Tween.Set(ref value, 3.0f, 2.0f);

				ship.Transform.Move(new Vector2(3.0f, -3.0f), 5.0f)
					.OnComplete(OnTweenComplete);
			}
		}

		private void OnTweenComplete()
		{
			Debug.Log($"Complete tween");
		}
	}
}