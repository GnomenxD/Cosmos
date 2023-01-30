using System;

namespace CosmosFramework
{
	public static class Extensions
	{
		public static void ForEach<T>(this System.Array array, Action<T> action)
		{
			for(int i = 0; i < array.Length; i++)
			{
				action.Invoke((T)array.GetValue(i));
			}
		}
		public static T[] EnsureCapacity<T>(this T[] items, int capacity)
		{
			if (capacity < items.Length)
				return items;
			int length = Math.Max((int)((double)items.Length * 1.5), capacity);
			T[] collection = items;
			items = new T[length];
			Array.Copy((Array)collection, 0, (Array)items, 0, items.Length);
			return items;
		}
	}
}