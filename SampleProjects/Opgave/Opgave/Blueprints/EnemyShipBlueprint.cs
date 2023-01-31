using CosmosFramework;

namespace Opgave.Blueprints
{
	internal class EnemyShipBlueprint : Blueprint<Ship, EnemyShipBlueprint>
	{
		protected override void Create()
		{
			Name = "Enemy Ship";
			Ship ship = AddComponent<Ship>();
			SpriteRenderer renderer = AddComponent<SpriteRenderer>();
			renderer.Sprite = Assets.EnemyBlack1;
		}

		protected override void Initialize(Ship blueprint, BlueprintParam param)
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