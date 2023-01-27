using System.Collections.Generic;
using System.Reflection;

namespace CosmosFramework.Netcode.Collections
{
	public class NetcodeBlueprintCollection
	{
		private readonly List<BlueprintBase> blueprints = new List<BlueprintBase>();

		public void Register<T>() where T : BlueprintBase
		{
			PropertyInfo info = typeof(T).GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
			if (info == null)
			{
				Debug.Log($"Could not find correct singleton of type({typeof(T).Name}).", LogFormat.Error);
				return;
			}
			T instance = (T)info.GetValue(null, null);
			blueprints.Add(instance);
		}
	}
}