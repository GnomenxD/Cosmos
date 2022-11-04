using CosmosEngine;
using CosmosEngine.Factory;

namespace SpaceShooter
{
	internal class ProjectileFactory : PrefabFactory<Projectile>
	{
		protected override GameObject SetPrefab()
		{
			GameObject obj = new GameObject("Projectile");
			obj.AddComponent<Projectile>();
			obj.AddComponent<SpriteRenderer>().Sprite = ArtContent.EffectYellow;
			CircleCollider collider = obj.AddComponent<CircleCollider>();
			collider.IsTriggerOnly = true;
			collider.Radius = 0.2f;
			collider.Offset = new Vector2(0, -0.15f);
			obj.AddComponent<Rigidbody>().IsKinematic = true;
			obj.Transform.LocalScale = new Vector2(0.15f, 0.5f);
			obj.Enabled = false;
			return obj;
		}

		public override Projectile Create()
		{
			return Instantiate<Projectile>(Prefab);
		}
	}
}