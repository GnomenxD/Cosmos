using CosmosEngine.Factory;
using CosmosEngine.ObjectPooling;

namespace SpaceShooter
{
	public class ProjectilePool : ObjectPool<Projectile>
	{
		private ProjectileFactory factory;
		protected override IFactory<Projectile> Factory => factory ??= new ProjectileFactory();

		public override void Return(Projectile item)
		{
			item.GameObject.Enabled = false;
			base.Return(item);
		}
	}
}