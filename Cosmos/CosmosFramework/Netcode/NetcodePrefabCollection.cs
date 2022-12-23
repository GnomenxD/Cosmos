using System.Collections.Generic;

namespace CosmosFramework
{
	public class NetcodePrefabCollection
	{
		private readonly List<BlueprintBase> prefabObjects = new List<BlueprintBase>();

		public void Register<T>() where T : BlueprintBase
		{
			prefabObjects.Add(System.Activator.CreateInstance<T>());
		}
	}
}