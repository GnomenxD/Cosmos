using CosmosFramework;

namespace Opgave.Prefabs
{
	internal class EnemyShipBlueprint : Blueprint<PlayerShip, EnemyShipBlueprint>
	{
		protected override void Create(BlueprintParam param)
		{
			Name = "Enemy Ship";
			PlayerShip ship = AddComponent<PlayerShip>();
			ship.Health = param.ReadValue<int>();

			SpriteRenderer renderer = AddComponent<SpriteRenderer>();
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