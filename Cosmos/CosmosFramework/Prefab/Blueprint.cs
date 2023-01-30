namespace CosmosFramework
{
	public abstract class Blueprint<TBlueprint> : BlueprintBase where TBlueprint : Blueprint<TBlueprint>, new()
	{
		private static TBlueprint instance;
		protected static TBlueprint Instance => instance ??= new TBlueprint();

		public static GameObject Instantiate() => Instantiate(Vector2.Zero);

		public static GameObject Instantiate(Vector2 position) => Instantiate(position, 0.0f);

		public static GameObject Instantiate(Vector2 position, float rotation, params object[] param) => Instance.InstantiatePrefab(position, rotation, param);

		public GameObject InstantiatePrefab(Vector2 position, float rotation, params object[] param)
		{
			if (!Instantiated)
			{
				Create();
				Instantiated = true;
			}
			GameObject clone = Prefab.CreateFromBlueprint(Name, Components);
			clone.Enabled = true;
			clone.Transform.Position = position;
			clone.Transform.Rotation = rotation;
			return clone;
		}
	}

	public abstract class Blueprint<T, TBlueprint> : Blueprint<TBlueprint> where TBlueprint : Blueprint<T, TBlueprint>, new() where T : Component
	{
		protected virtual void Initialize(T blueprint, BlueprintParam param)
		{

		}
		public static new T Instantiate() => Instantiate(Vector2.Zero);

		public static new T Instantiate(Vector2 position) => Instantiate(position, 0.0f);

		public static new T Instantiate(Vector2 position, float rotation, params object[] param) => Instance.InstantiatePrefab(position, rotation, param);

		public new T InstantiatePrefab(Vector2 position, float rotation, params object[] param)
		{
			if (!Instantiated)
			{
				Create();
				Instantiated = true;
			}
			GameObject clone = Prefab.CreateFromBlueprint(Name, Components);
			clone.Enabled = true;
			clone.Transform.Position = position;
			clone.Transform.Rotation = rotation;
			T blueprint = clone.GetComponent<T>();
			Initialize(blueprint, new BlueprintParam(param));
			return blueprint;
		}
	}
}