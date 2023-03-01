using CosmosFramework;
using CosmosFramework.EventSystems;
using System.Collections;
using System.Threading.Tasks;

namespace Opgave
{
	public class GameWorld : CosmosFramework.CoreModule.Game
	{
		private PlayerShip ship;

		public override void Initialize()
		{
		}

		public override void Start()
		{
			ship = Blueprints.PlayerShipBlueprint.Instantiate();
			if(ship.TryGetComponent(out SpriteRenderer sr))
			{
				sr.Sprite = Assets.PlayerShip1Green;
			}
			ship.StartMove();
			
			Observer.Once(ship, (i) => { return i.Health <= 50; }, PrintShipHealth);
		}

		private void PrintShipHealth(PlayerShip ship)
		{
			Debug.Log($"{ship.Health:F0}");
		}

		private IEnumerator ProcessLowHealthShip(PlayerShip ship)
		{
			Debug.Log("Ship has low health");
			yield return Wait.While(() => { return ship.Health > 0; });
			Debug.Log($"Ship now has no health - destroy it.");
		}

		public override void Update()
		{
			Debug.QuickLog($"{ship.Health:F0}");
			ship.Health -=  10.0f * Time.DeltaTime;
		}
	}
}