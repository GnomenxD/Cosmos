using CosmosFramework;

namespace Opgave.Blueprints
{
	internal class EnemyShipBlueprint : Blueprint<PlayerShip, EnemyShipBlueprint>
	{
		protected override void Create()
		{
			Name = "Enemy Ship";
			PlayerShip ship = AddComponent<PlayerShip>();
			SpriteRenderer renderer = AddComponent<SpriteRenderer>();
			renderer.Sprite = Assets.EnemyBlack1;
		}

		protected override void Initialize(PlayerShip blueprint, BlueprintParam param)
		{
			blueprint.Health = param.ReadValue<int>(100);
			SpriteRenderer renderer = blueprint.AddComponent<SpriteRenderer>();
			renderer.Sprite = param.ReadValue<string>() switch
			{
				"red" => Assets.EnemyRed3,
				"green" => Assets.EnemyGreen4,
				"blue" => Assets.EnemyBlue2,
				_ => Assets.EnemyBlack1,
			};
		}
	}
}