using CosmosEngine;

namespace SpaceShooter
{
	public class Enemy : GameBehaviour
	{
		private int health;
		private int damage;

		public int Health => health;
		public int Damage => damage;

		protected override void Awake()
		{
			base.Awake();

		}

		protected override void OnInstantiated()
		{
			health = Random.Range(50, 200);
			damage = Random.Range(10, 25);
		}
	}
}