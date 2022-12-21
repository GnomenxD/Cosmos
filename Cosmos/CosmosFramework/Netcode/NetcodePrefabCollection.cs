using System.Collections.Generic;

namespace CosmosFramework
{
	public class NetcodePrefabCollection
	{
		private readonly List<PrefabObjectBase> prefabObjects = new List<PrefabObjectBase>();

		public void Register<T>() where T : PrefabObjectBase
		{
			prefabObjects.Add(System.Activator.CreateInstance<T>());
		}
	}
}