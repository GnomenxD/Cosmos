using CosmosFramework;

namespace Opgave.Blueprints
{
	internal class PlayerShipBlueprint : Blueprint<Ship, PlayerShipBlueprint>
	{
		protected override void Create()
		{
			Name = "Player Ship";
			Ship ship = AddComponent<Ship>();
			ship.PlayerControlled = true;
			SpriteRenderer renderer = AddComponent<SpriteRenderer>();
			renderer.Sprite = Assets.PlayerShip1Orange;
		}

		protected override void Initialize(Ship blueprint, BlueprintParam param)
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