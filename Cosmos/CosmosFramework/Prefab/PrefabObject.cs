using CosmosFramework.Modules;
using System.Collections.Generic;

namespace CosmosFramework
{
	public abstract class PrefabObject<T> : PrefabObjectBase where T : PrefabObject<T>, new()
	{
		private static T instance;
		protected static T Instance => instance ??= new T();

		public static GameObject Instantiate() => Instantiate(Vector2.Zero);

		public static GameObject Instantiate(Vector2 position) => Instantiate(position, 0.0f);

		public static GameObject Instantiate(Vector2 position, float rotation, params object[] param) => Instance.InstantiatePrefab(position, rotation, param);

		public GameObject InstantiatePrefab(Vector2 position, float rotation, params object[] param)
		{
			if (!Instantiated)
			{
				Create(new PrefabParams(param));
				Instantiated = true;
			}
			GameObject clone = Prefab.CreateFromPrefabObject(Name, Components);
			clone.Enabled = true;
			clone.Transform.Position = position;
			clone.Transform.Rotation = rotation;
			return clone;
		}
	}

	public abstract class PrefabObject<TComponent, T> : PrefabObject<T> where T : PrefabObject<TComponent, T>, new() where TComponent : Component
	{
		public static new TComponent Instantiate() => Instantiate(Vector2.Zero);

		public static new TComponent Instantiate(Vector2 position) => Instantiate(position, 0.0f);

		public static new TComponent Instantiate(Vector2 position, float rotation, params object[] param) => Instance.InstantiatePrefab(position, rotation, param);

		public new TComponent InstantiatePrefab(Vector2 position, float rotation, params object[] param)
		{
			if (!Instantiated)
			{
				Create(new PrefabParams(param));
				Instantiated = true;
			}
			GameObject clone = Prefab.CreateFromPrefabObject(Name, Components);
			clone.Enabled = true;
			clone.Transform.Position = position;
			clone.Transform.Rotation = rotation;
			return clone.GetComponent<TComponent>();
		}
	}
}