using CosmosFramework;
using System.Collections;

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

		protected override void Start()
		{

		}

		public void StartMove()
		{
			StartRoutine(MoveAnimation, Vector2.Zero, new Vector2(8, -4), new Vector2(0, 4));
		}

		private IEnumerator MoveAnimation(Vector2 start, Vector2 middle, Vector2 end)
		{
			Transform.Position = start;

			while (Vector2.Distance(Transform.Position, middle) > 0.5f)
			{
				Transform.Position = Vector2.MoveTowards(Transform.Position, middle, 3.0f * Time.DeltaTime);
				yield return null;
			}

			while (Vector2.Distance(Transform.Position, end) > 0.5f)
			{
				Transform.Position = Vector2.MoveTowards(Transform.Position, end, 3.0f * Time.DeltaTime);
				yield return null;
			}

			Debug.Log($"I reached my end point.");
		}

		protected override void Update()
		{
			//Debug.Log($"Ship: {this} | {Health}");

			if (!PlayerControlled)
				return;

			Vector2 input = new Vector2(
				InputManager.GetAxis("Horizontal"),
				InputManager.GetAxis("Vertical"));

			Transform.Translate(input * speed * Time.DeltaTime);
		}
	}
}