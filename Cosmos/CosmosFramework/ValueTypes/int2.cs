using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct int2 : IComparable<int2>, IEquatable<int2>
	{
		private readonly int x;
		private readonly int y;

		public int X => x;
		public int Y => y;

		public int this[int index]
		{
			get
			{
				return index switch
				{
					0 => X,
					1 => Y,
					_ => throw new IndexOutOfRangeException(),
				};
			}
		}

		public int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public int GetLength(int index) => index switch
		{
			0 => X,
			1 => Y,
			_ => throw new IndexOutOfRangeException(),
		};

		public void For(Action<int, int> action)
		{
			for(int x = 0; x < this.x; x++)
			{
				for(int y = 0; y < this.y; y++)
				{
					action.Invoke(x, y);
				}
			}
		}

		public int CompareTo(int2 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y);

		public bool Equals(int2 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is int2))
				return false;
			else
				return Equals((int2)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

		public override string ToString() => $"({X}, {Y})";

		public static bool operator ==(int2 lhs, int2 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(int2 lhs, int2 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(int2 lhs, int2 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(int2 lhs, int2 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(int2 lhs, int2 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(int2 lhs, int2 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator int2(int[] array)
		{
			return new int2(array[0], array[1]);
		}
	}
}