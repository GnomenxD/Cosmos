#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CosmosEngine.Collections
{
	/// <summary>
	/// A <see cref="CosmosEngine.Collections.RandomAssortment{T}"/> is an unsorted collection, which allows for easy random access to elements stored.
	/// </summary>
	/// <typeparam name="T">The element type represented in the collection.</typeparam>
	public class RandomAssortment<T>
	{
		private T previousItem;
		private T[] collection;
		private bool[] occupiedIndex;
		private int span;
		private int count;
		private bool nonSequential;
		
		/// <summary>
		/// The amount of useable items in the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		public int Count => count;
		/// <summary>
		/// The total amount of items in the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		public int Span => span;
		/// <summary>
		/// Wether the item returned when using Peek() is allowed to be the same as the previous.
		/// <para>If <see langword="true"/>: Will ensured that the newest item returned does not match the previous item.</para>
		/// </summary>
		public bool NonSequential { get => nonSequential; set => nonSequential = value; }

		/// <summary>
		/// Creates a new empty <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		public RandomAssortment()
		{
			collection = new T[8];
			occupiedIndex = new bool[8];
		}
		/// <summary>
		/// Create a new <see cref="CosmosEngine.Collections.RandomAssortment{T}"/> with elements copied from an existing collection.
		/// </summary>
		/// <param name="collection"></param>
		public RandomAssortment(IEnumerable<T> collection)
		{
			this.collection = collection.ToArray();
			occupiedIndex = new bool[this.collection.Length];
			span = collection.Count();
			count = span;
		}

		/// <summary>
		/// Ensures the capacity of the collections.
		/// </summary>
		/// <param name="capacity"></param>
		private void EnsureCapacity(int capacity)
		{
			EnsureCapacity(ref collection, capacity);
			EnsureCapacity(ref occupiedIndex, capacity);
		}

		/// <summary>
		/// Ensures the capacity (length) of <paramref name="array"/> to be greater than <paramref name="capacity"/>. This method will increase the length of any array while persisting the items in the array.
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <param name="array"></param>
		/// <param name="capacity"></param>
		private static void EnsureCapacity<T1>(ref T1[] array, int capacity)
		{
			if (capacity < array.Length)
				return;
			int length = Mathf.Max((int)(array.Length * 1.5f), capacity);
			T1[] temp = array;
			array = new T1[length];
			Array.Copy((Array)temp, 0, (Array)array, 0, temp.Length);
		}

		/// <summary>
		/// Adds the <paramref name="item"/> to the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		/// <param name="item"></param>
		public void Add(T item)
		{
			EnsureCapacity(span + 1);
			collection[span] = item;
			++span;
			count++;
		}

		/// <summary>
		/// Adds a <paramref name="collection"/> of items to the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		/// <param name="collection"></param>
		public void Add(IEnumerable<T> collection)
		{
			EnsureCapacity(span + collection.Count());
			for(int i = 0; i < collection.Count(); i++)
			{
				Add(collection.ElementAt(i));
			}
		}

		/// <summary>
		/// Allows the use of <paramref name="func"/> to add new items to the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		/// <param name="func"></param>
		/// <param name="amount"></param>
		public void Fill(Func<T> func, int amount)
		{
			if (amount <= 0)
				return;

			List<T> list = new List<T>();
			for (int i = 0; i < amount; i++)
				list.Add(func.Invoke());
			Add(list);
		}

		/// <summary>
		/// Returns a random <typeparamref name="T"/> from the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		/// <returns></returns>
		public T Peek()
		{
			int index;

			if(nonSequential && previousItem != null)
			{
				Debug.Log($"NonSequential Peek()");
				int maxAttempts = Span * 2;
				do
				{
					index = Random.Range(0, Span);
					if (maxAttempts < 0)
					{
						Debug.LogWarning($"RandomAssortment is set to Non Sequential, but was not possible to Peek an object that didn't match the previous one.");
						break;
					}
					maxAttempts--;
				} while (collection[index].Equals(previousItem));
			}
			else
			{
				index = Random.Range(0, Span);
			}
			previousItem = collection[index];
			return collection[index];
		}

		/// <summary>
		/// Returns a collection with <paramref name="amount"/> random <typeparamref name="T"/> from the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="allowDuplicates">Not allowing duplicates can decrease the total amount of <typeparamref name="T"/> returned, but will ensure that all items are unique.</param>
		/// <returns></returns>
		public T[] PeekRange(int amount, bool allowDuplicates = false)
		{
			if (span <= 0)
			{
				return Array.Empty<T>();
			}
			if (amount > count)
			{
				Debug.LogWarning($"Trying to peek a range from RandomAssortment with {amount} elements, collection only contains {Span} items.");
				amount = count;
			}
			List<T> result = new List<T>();

			bool duplicate = false;
			bool reducedCollection = false;
			for (int i = 0; i < amount; i++)
			{
				T item = Peek();

				if (!allowDuplicates)
				{
					int maxAttempts = Span * 2;
					do
					{
						if (maxAttempts < 0)
							break;
						maxAttempts--;

						duplicate = false;
						item = Peek();
						foreach (T element in result)
						{
							if (item.Equals(element))
							{
								duplicate = true;
								break;
							}
						}
					} while (duplicate);

					if(duplicate)
					{
						reducedCollection = true;
						continue;
					}
				}

				result.Add(item);
			}
			if(reducedCollection)
			{
				Debug.LogWarning($"Peek range request on RandomAssortment for {amount} elements was incomplete. Not enough unique items exist, collection may be smaller than requested.");
			}

			return result.ToArray();
		}

		/// <summary>
		/// Returns a random <typeparamref name="T"/> from the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>. An item returned by Get will be marked as USED and will not be returned again, unless Reset is invoked.
		/// </summary>
		/// <returns></returns>
		public T? Get()
		{
			if(count <= 0)
			{
				Debug.LogError($"IndexOutOfRange for RandomAssortment - this can be the result of all possible elements have been used already.");
				return default(T);
			}

			int index = 0;
			do
			{
				if (count <= 0)
					return default(T);

				index = Random.Range(0, Span);
			} while (occupiedIndex[index]);

			T item = collection[index];
			occupiedIndex[index] = true;
			previousItem = item;
			count--;

			return item;
		}

		/// <summary>
		/// Returns a collection with <paramref name="amount"/> random <typeparamref name="T"/> from the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>. Items returned by Get will be marked as USED and will not be returned again, unless Reset is invoked.
		/// </summary>
		/// <param name="amount"></param>
		/// <returns></returns>
		public T[] GetRange(int amount)
		{
			if(span <= 0)
			{
				return Array.Empty<T>();
			}
			if(amount > count)
			{
				Debug.LogWarning($"Trying to get a range from RandomAssortment with {amount} elements, Span only consist of {count} items. An adjusted collection will be returned.");
				amount = count;
			}
			T[] result = new T[amount];
			for(int i = 0; i < amount; i++)
			{
				result[i] = Get();
			}
			return result;
		}

		/// <summary>
		/// Removes the first occurrence of <paramref name="item"/> found in the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		/// <param name="item"></param>
		public void Remove(T item)
		{
			int index = -1;
			for (int i = 0; i < Span; i++)
			{
				if (collection[i].Equals(item))
				{
					index = i;
					break;
				}
			}
			if(index >= 0)
			{
				//Remove
			}
		}

		/// <summary>
		/// Removes all elements from the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/> that matches the <paramref name="predicate"/>.
		/// </summary>
		/// <param name="predicate"></param>
		public void RemoveAll(Predicate<T> predicate)
		{
			List<int> indexing = new List<int>();
			for(int i = 0; i < Span; i++)
			{
				if (predicate.Invoke(collection[i]))
				{
					indexing.Add(i);
				}
			}

			foreach (int index in indexing)
			{
				//Remove
			}
		}

		/// <summary>
		/// Resets the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>, allowing all elements in collection to be once again returend using the Get method.
		/// </summary>
		public void Reset()
		{
			count = Span;
			for (int i = 0; i < occupiedIndex.Length; i++)
			{
				occupiedIndex[i] = false;
			}
		}

		/// <summary>
		/// Removes all elements from the <see cref="CosmosEngine.Collections.RandomAssortment{T}"/>.
		/// </summary>
		public void Clear()
		{
			collection = new T[8];
			span = 0;
			Reset();
		}
	}
}