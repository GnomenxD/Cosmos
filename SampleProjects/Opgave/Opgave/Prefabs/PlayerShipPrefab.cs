using CosmosFramework;

namespace Opgave.Prefabs
{
	internal class PlayerShipPrefab : PrefabObject<PlayerShip, PlayerShipPrefab>
	{
		protected override void Create(PrefabParams param)
		{
			Name = "Player Ship";
			PlayerShip ship = AddComponent<PlayerShip>();
			ship.Health = param.ReadValue<int>();
			ship.Speed = param.ReadValue<int>();
			ship.Health = param.ReadValue<int>();
			ship.PlayerControlled = true;

			SpriteRenderer renderer = AddComponent<SpriteRenderer>();
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