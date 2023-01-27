using CosmosFramework;

namespace Opgave.Blueprint
{
	internal class PlayerShipBlueprint : Blueprint<PlayerShip, PlayerShipBlueprint>
	{
		protected override string UniqueName => "Player Ship";

		protected override void Create()
		{
			PlayerShip ship = AddComponent<PlayerShip>();
			ship.PlayerControlled = true;
			SpriteRenderer renderer = AddComponent<SpriteRenderer>();
			renderer.Sprite = Assets.PlayerShip1Orange;
		}

		protected override void Initialize(ref PlayerShip blueprint, BlueprintParam param)
		{
			blueprint.Health = param.ReadValue<int>(100);
			blueprint.Speed = param.ReadValue<int>(5);
			blueprint.Damage = param.ReadValue<int>(10);

			SpriteRenderer renderer = blueprint.GetComponent<SpriteRenderer>();
			renderer.Sprite = param.ReadValue<string>() switch
			{
				"red" => Assets.PlayerShip1Red,
				"green" => Assets.PlayerShip1Green,
				"blue" => Assets.PlayerShip1Blue,
				_ => Assets.PlayerShip1Orange,
			};
		}
	}
}