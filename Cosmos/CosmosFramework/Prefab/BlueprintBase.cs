using CosmosFramework.CoreModule;
using System.Collections.Generic;

namespace CosmosFramework
{
	/// <summary>
	/// The base for <see cref="CosmosFramework.Blueprint{TFactory}"/> and <see cref="CosmosFramework.Blueprint{TBlueprint, TFactory}"/>, do not inherit directly from this class.
	/// </summary>
	public abstract class BlueprintBase
	{
		private bool instantiated;
		private readonly List<Component> components = new List<Component>();

		/// <summary>
		/// The name given to a prefab when it's instantiated, should be differentiating from other Blueprints.
		/// </summary>
		protected abstract string UniqueName { get; }
		/// <summary>
		/// Whether the initial prefab has been created to this blueprint.
		/// </summary>
		protected private bool Instantiated { get => instantiated; set => instantiated = value; }
		/// <summary>
		/// The complete list of <see cref="CosmosFramework.Component"/> on the prefab.
		/// </summary>
		protected private List<Component> Components => components;

		public BlueprintBase()
		{
			Debug.Log($"New blueprint factory {{{GetType().Name}}}");
		}

		/// <summary>
		/// Initial setup and "constructor" for the prefab. Use to populate the <see cref="CosmosFramework.GameObject"/> with <see cref="CosmosFramework.Component"/> by using the <see cref="CosmosFramework.BlueprintBase.AddComponent{T}(object?[]?)"/> method. Initial values can also be decalared in the creation setup.
		/// </summary>
		protected abstract void Create();

		/// <summary>
		/// Adds <typeparamref name="T"/> to the components that is attached to the prefab when it's instantiated.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If <paramref name="args"/> is an empty array or <see langword="null"/>, the constructor that takes no parameters (the parameterless constructor) is invoked.</param>
		/// <exception cref="System.MissingMethodException"></exception>
		/// <returns></returns>
		protected T AddComponent<T>(params object?[]? args) where T : Component
		{
			T component = System.Activator.CreateInstance(typeof(T), args) as T;
			if (component != null)
				components.Add(component);
			return component;
		}

		/// <summary>
		/// Instantiates the blueprint.
		/// </summary>
		/// <param name="position">The position the <see cref="CosmosFramework.GameObject"/> is instantiated.</param>
		/// <param name="rotation">The rotation of the <see cref="CosmosFramework.GameObject"/>.</param>
		/// <param name="param">A set of parameters that can be used when instantiating the blueprint.</param>
		/// <returns>A new <see cref="CosmosFramework.GameObject"/> instance, containing the same components and values as the blueprint.</returns>
		public abstract GameObject InstantiatePrefab(Vector2 position, float rotation, params object[] param);
	}
}