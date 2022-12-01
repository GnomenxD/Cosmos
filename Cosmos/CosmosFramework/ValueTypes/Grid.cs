using System;
using System.Collections;
using System.Collections.Generic;

namespace CosmosFramework
{
	public readonly struct Grid<T> : IEnumerable<T>, IEquatable<Grid<T>>
	{
		private readonly T[,] collection;
		private readonly int count;
		public T this[int x, int y]
		{
			get
			{
				return collection[x, y];
			}
			set
			{
				this.collection[x, y] = value;
			}
		}
		public int Count => count;
		public Grid(T[,] collection)
		{
			this.collection = collection;
			if (collection == null)
			{
				Debug.Log($"");
				this.count = 0;
			}
			else
				this.count = collection.GetLength(0) + collection.GetLength(1);
		}

		public Grid(int x, int y)
		{
			this.collection = new T[x, y];
			this.count = x + y;
		}

		public int Length(int index) => index switch
		{
			0 => collection.GetLength(0),
			1 => collection.GetLength(1),
			_ => throw new IndexOutOfRangeException(),
		};

		public void For(Action<int, int, T> action)
		{
			for(int x = 0; x < Length(0); x++)
			{
				for(int y = 0; y < Length(1); y++)
				{
					action.Invoke(x, y, collection[x, y]);
				}
			}
		}

		public void For(Action<int, int> action)
		{
			for (int x = 0; x < Length(0); x++)
			{
				for (int y = 0; y < Length(1); y++)
				{
					action.Invoke(x, y);
				}
			}
		}

		public void For(Action<T> action)
		{
			for (int x = 0; x < Length(0); x++)
			{
				for (int y = 0; y < Length(1); y++)
				{
					action.Invoke(collection[x, y]);
				}
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Grid<T>))
				return false;
			else
				return Equals((Grid<T>)obj);
		}

		public override int GetHashCode()
		{
			int hash = 0x2D2816FE;
			int max = Math.Min(Count, 16);
			for (int x = 0; x != max; ++x)
			{
				for (int y = 0; y != max; ++y)
				{
					var item = this[x, y];
					hash = hash * 31 + (item == null ? 0 : item.GetHashCode());
				}
			}
			return hash;
		}

		public bool Equals(Grid<T> other)
		{
			if (collection == null)
				return false;
			if (other.collection == null)
				return false;
			foreach(T item in collection)
			{
				foreach(T otherItem in other.collection)
				{
					if(!item.Equals(otherItem))
					{
						return false;
					}
				}
			}
			return true;
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int x = 0; x < Length(0); x++)
			{
				for (int y = 0; y < Length(1); y++)
				{
					yield return this[x, y];
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			for (int x = 0; x < Length(0); x++)
			{
				for (int y = 0; y < Length(1); y++)
				{
					yield return this[x, y];
				}
			}
		}

		public static bool operator ==(Grid<T> lhs, object rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Grid<T> lhs, object rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator ==(Grid<T> lhs, Grid<T> rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Grid<T> lhs, Grid<T> rhs)
		{
			return !(lhs == rhs);
		}

		public static implicit operator Grid<T>(T[,] array)
		{
			return new Grid<T>(array);
		}
	}
}