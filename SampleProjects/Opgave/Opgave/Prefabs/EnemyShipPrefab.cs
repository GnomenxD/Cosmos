using CosmosFramework;

namespace Opgave.Prefabs
{
	internal class EnemyShipPrefab : PrefabObject<PlayerShip, EnemyShipPrefab>
	{
		protected override void Create(PrefabParams param)
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