using System;
using System.Collections.Generic;
using System.Linq;

namespace CosmosEngine.Collections
{
	public class RandomAssortment<T>
	{
		private T[] collection;
		private int[] occupiedIndex;
		private int count;

		public RandomAssortment() => collection = Array.Empty<T>();
		public RandomAssortment(IEnumerable<T> collection)
		{
			this.collection = collection.ToArray();
			count = collection.Count();
		}

		public int Count => count;

		private void EnsureCapacity(int capacity)
		{
			if (capacity < collection.Length)
				return;
			int length = Math.Max((int)((double)collection.Length * 1.5), capacity);
			T[] temporary = collection;
			collection = new T[length];
			Array.Copy((Array)temporary, 0, (Array)collection, 0, collection.Length);
		}

		public void Reset()
		{

		}

		public void Clear() => collection = Array.Empty<T>();

		public void Add(T item)
		{
			EnsureCapacity(count + 1);
			collection[count] = item;
			++count;
		}

		public void Add(IEnumerable<T> collection)
		{
			int max = count + collection.Count();
			int index = 0;
			EnsureCapacity(max);
			for(int i = count; i < max; i++)
			{
				this.collection[i] = collection.ElementAt(index);
				index++;
			}
		}

		public T Peek()
		{
			int index = Random.Range(0, collection.Length);
			return collection[index];
		}

		public T[] PeekRange(int count, bool duplicates = false)
		{

		}

		public T Get()
		{
			int index = Random.Range(0, Count);
			T item = collection[index];
			collection.RemoveAt(index);
			return item;
		}

		public T[] GetRange(int count, bool duplicates = false)
		{
			T[] result = new T[count];

		}
	}
}