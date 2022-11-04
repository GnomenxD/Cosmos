using CosmosEngine;
using CosmosEngine.Modules;
using System;

namespace SpaceShooter
{
	public class ProjectileSystem : ObserverManager<Projectile, ProjectileSystem>
	{
		public override void Initialize()
		{
			base.Initialize();
			ObjectDelegater.CreateNewDelegation<Projectile>(Subscribe);
		}

		public override void BeginEventCall()
		{
			foreach(Projectile p in observerList)
			{
				if(p.Expired)
				{
					observerList.IsDirty = true;
					continue;
				}
				if (p.Enabled)
				{
					if (p.Lifespan > 0) 
					{
						p.Transform.Translate(p.Speed * Time.DeltaTime * Vector2.Up, Space.Self);
						p.Lifespan -= Time.DeltaTime;
						if(p.Lifespan < 0)
							p.Return();
					}
				}
			}
		}

		public override Predicate<Projectile> RemoveAllPredicate()
		{
			return item => item.Expired;
		}
	}
}