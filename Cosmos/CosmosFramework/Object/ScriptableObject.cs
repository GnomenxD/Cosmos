
using CosmosFramework.CoreModule;
using System.Collections.Generic;

namespace CosmosFramework
{
	public abstract class ScriptableObject : Object
	{
		private static Dictionary<System.Type, ScriptableObject> scriptableObjects = new Dictionary<System.Type, ScriptableObject>();

		~ScriptableObject()
		{
			ClearInstance(GetType());
		}

		public static T Instance<T>() where T : ScriptableObject, new() => Instance(typeof(T)) as T;

		public static ScriptableObject Instance(System.Type type)
		{
			if (!scriptableObjects.TryGetValue(type, out ScriptableObject instance))
			{
				//instance does not exist and must be created.
				instance = (ScriptableObject)System.Activator.CreateInstance(type);
				scriptableObjects.Add(type, instance);
			}
			return instance;
		}

		internal static void ClearInstance<T>() => ClearInstance(typeof(T));
		internal static void ClearInstance(System.Type type)
		{
			if (scriptableObjects.ContainsKey(type))
				scriptableObjects.Remove(type);
		}
	}
}