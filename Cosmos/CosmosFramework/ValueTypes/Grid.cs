using System;

namespace CosmosFramework
{
	public readonly struct Grid<T> : IEquatable<Grid<T>>
	{
		private readonly T[,] value;
		public T this[int x, int y]
		{
			get
			{
				return value[x, y];
			}
			set
			{
				_ = this.value[x, y] = value;
			}
		}
		public Grid(T[,] value ) => this.value = value;
		public Grid(int x, int y) => value = new T[x, y];

		public void Populate(int x, int y, T v) => value[x,y] = v;

		public int GetLength(int index) => index switch
		{
			0 => value.GetLength(0),
			1 => value.GetLength(1),
			_ => throw new IndexOutOfRangeException(),
		};

		public void For(Action<int, int, T> action)
		{
			for(int x = 0; x < GetLength(0); x++)
			{
				for(int y = 0; y < GetLength(1); y++)
				{
					action.Invoke(x, y, value[x, y]);
				}
			}
		}

		public void For(Action<T> action)
		{
			for (int x = 0; x < GetLength(0); x++)
			{
				for (int y = 0; y < GetLength(1); y++)
				{
					action.Invoke(value[x, y]);
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

		public bool Equals(Grid<T> other)
		{
			if (value == null)
				return false;
			if (other.value == null)
				return false;
			foreach(T item in value)
			{
				foreach(T otherItem in other.value)
				{
					if(!item.Equals(otherItem))
					{
						return false;
					}
				}
			}
			return true;
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