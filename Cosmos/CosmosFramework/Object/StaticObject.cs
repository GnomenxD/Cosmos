using System.Collections.Generic;

namespace CosmosFramework
{
	public abstract class StaticObject<T> where T : StaticObject<T>, new()
	{
		private static Dictionary<System.Type, StaticObject<T>> staticObjects = new Dictionary<System.Type, StaticObject<T>>();

		private static T instance;
		public static T Instance => instance ??= new();
	}
}