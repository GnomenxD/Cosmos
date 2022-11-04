using CosmosEngine;
using CosmosEngine.ObjectPooling;
using System.Collections;
using System.Collections.Generic;

namespace SpaceShooter
{
	public class Projectile : GameBehaviour
	{
		private float speed;
		private float lifespan;
		private IPool<Projectile> pool;

		public float Speed => speed;
		public float Lifespan { get => lifespan; set => lifespan = value; }


		public void Activate(ProjectilePool pool)
		{
			this.speed = 2.56f;
			this.pool = pool; 
			lifespan = 10.0f;
		}

		public void Activate(ProjectilePool pool, float speed)
		{
			this.speed = speed;
			this.pool = pool;
			lifespan = 10.0f;
		}

		protected override void OnTriggerEnter(Collider other)
		{
			
		}

		public void Return()
		{
			pool.Return(this);
		}
	}
}