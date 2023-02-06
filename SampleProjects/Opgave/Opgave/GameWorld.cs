using CosmosFramework;

namespace Opgave
{
	public class GameWorld : CosmosFramework.CoreModule.Game
	{
		public override void Initialize()
		{
		}

		public override void Start()
		{
			PlayerShip ship = Blueprints.PlayerShipBlueprint.Instantiate();
			if(ship.TryGetComponent(out SpriteRenderer sr))
			{
				sr.Sprite = Assets.PlayerShip1Green;
			}
			ship.StartMove();
		}

		public override void Update()
		{
		}
	}
}