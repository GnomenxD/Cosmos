using CosmosFramework;
using Opgave.Prefabs;

namespace Opgave
{
	internal class PlayerShip : GameBehaviour
	{
		private float health;
		private float speed;
		private float damage;
		private bool playerControlled;

		public float Health { get => health; set => health = value; }
		public float Speed { get => speed; set => speed = value; }
		public float Damage { get => damage; set => damage = value; }
		public bool PlayerControlled { get => playerControlled; set => playerControlled = value; }

		protected override void Update()
		{
			Debug.Log($"Ship: {this} | {Health}");

			if (!PlayerControlled)
				return;

			Vector2 input = new Vector2(
				InputManager.GetAxis("Horizontal"),
				InputManager.GetAxis("Vertical"));

			Transform.Translate(input * speed * Time.DeltaTime);
		}
	}
}