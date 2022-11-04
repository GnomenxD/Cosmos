using CosmosEngine;

namespace SpaceShooter
{
	internal class CameraRig : GameBehaviour
	{
		private Player player;

		public Player Player => player ??= FindObjectOfType<Player>();
		protected override void Update()
		{
			float distance = Vector2.Distance(Camera.Main.Position, Player.Transform.Position);
			Camera.Main.Position = Vector2.MoveTowards(Camera.Main.Position, Player.Transform.Position, distance * 2.2f * Time.DeltaTime);
		}
	}
}