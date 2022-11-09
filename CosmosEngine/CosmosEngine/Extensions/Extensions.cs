using System;

namespace CosmosEngine
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
	}
}