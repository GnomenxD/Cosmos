namespace CosmosFramework
{
	/// <summary>
	/// Blueprint Factory, used to generate a blueprint, which can then be used for easy instantiation of new instances of the same object.
	/// <para>Blueprint refers to a predefined <see cref="GameObject"/> that does not exist within the game loop. It can be populated with components and initial values.</para>
	/// </summary>
	/// <typeparam name="TFactory">The blueprint factory itself for singleton use.</typeparam>
	public abstract class Blueprint<TFactory> : BlueprintBase where TFactory : Blueprint<TFactory>
	{
		private static TFactory instance;
		public static TFactory Instance => instance ??= System.Activator.CreateInstance<TFactory>();

		/// <summary>
		/// Initialize is invoked just after the blueprint has been instantiated, it is used for a initial setup and changing values on the clone with the <paramref name="param"/>.
		/// </summary>
		/// <param name="clone">The new clone created from the blueprint.</param>
		/// <param name="param">A set of parameters that can be included when instantiating the blueprint. </param>
		protected virtual void Initialize(ref GameObject clone, BlueprintParam param)
		{

		}

		/// <summary>
		/// Instantiates a new instance of the blueprint.
		/// </summary>
		/// <returns>The </returns>
		public static GameObject Instantiate() => Instantiate(Vector2.Zero);

		/// <summary>
		/// Instantiates a new instance of the blueprint, with a specified <paramref name="position"/>.
		/// </summary>
		/// <returns>The </returns>
		public static GameObject Instantiate(Vector2 position) => Instantiate(position, 0.0f);

		/// <summary>
		/// Instantiates a new instance of the blueprint, with a specified <paramref name="position"/> and <paramref name="rotation"/>.
		/// </summary>
		/// <returns></returns>
		public static GameObject Instantiate(Vector2 position, float rotation, params object[] param) => Instance.InstantiatePrefab(position, rotation, param);

		public override GameObject InstantiatePrefab(Vector2 position, float rotation, params object[] param)
		{
			if (!Instantiated)
			{
				Create();
				Instantiated = true;
			}
			GameObject clone = Prefab.CreateFromBlueprint(UniqueName, Components);
			clone.Enabled = true;
			clone.Transform.Position = position;
			clone.Transform.Rotation = rotation;
			Initialize(ref clone, new BlueprintParam(param));
			return clone;
		}
	}

	/// <summary>
	/// <inheritdoc cref="CosmosFramework.Blueprint{TFactory}"/>
	/// </summary>
	/// <typeparam name="TBlueprint"></typeparam>
	/// <typeparam name="TFactory"></typeparam>
	public abstract class Blueprint<TBlueprint, TFactory> : Blueprint<TFactory> where TFactory : Blueprint<TBlueprint, TFactory> where TBlueprint : Component
	{
		protected override sealed void Initialize(ref GameObject clone, BlueprintParam param)
		{
			TBlueprint blueprint = clone.GetComponent<TBlueprint>();
			Initialize(ref blueprint, new BlueprintParam(param));
		}

		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Blueprint{TFactory}.Initialize(GameObject, BlueprintParam)"/>
		/// </summary>
		/// <param name="clone"></param>
		/// <param name="param"></param>
		protected virtual void Initialize(ref TBlueprint clone, BlueprintParam param)
		{

		}

		public static new TBlueprint Instantiate() => Instantiate(Vector2.Zero);

		public static new TBlueprint Instantiate(Vector2 position) => Instantiate(position, 0.0f);

		public static new TBlueprint Instantiate(Vector2 position, float rotation, params object[] param) => Instance.InstantiatePrefab(position, rotation, param).GetComponent<TBlueprint>();
	}
}