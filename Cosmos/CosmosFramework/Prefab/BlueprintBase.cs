using System.Collections.Generic;

namespace CosmosFramework
{
	public abstract class BlueprintBase
	{
		private bool instantiated;
		private string name;
		private readonly List<Component> components = new List<Component>();

		protected abstract void Create();

		protected string Name { get => name; set => name = value; }
		protected private bool Instantiated { get => instantiated; set => instantiated = value; }
		protected private List<Component> Components => components;

		protected TComponent AddComponent<TComponent>(params object?[]? args) where TComponent : Component
		{
			TComponent component = System.Activator.CreateInstance(typeof(TComponent), args) as TComponent;
			if (component != null)
				components.Add(component);

			return component;
		}

	}
}