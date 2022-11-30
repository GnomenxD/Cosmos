using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct int3 : IComparable<int3>, IEquatable<int3>
	{
		private readonly int x;
		private readonly int y;
		private readonly int z;

		public int X => X;
		public int Y => Y;
		public int Z => Z;

		public int this[int index]
		{
			get
			{
				return index switch
				{
					0 => X,
					1 => Y,
					2 => Z,
					_ => throw new IndexOutOfRangeException(),
				};
			}
		}

		public int3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public int GetLength(int index) => index switch
		{
			0 => X,
			1 => Y,
			2 => Z,
			_ => throw new IndexOutOfRangeException(),
		};

		public void For(Action<int, int, int> action)
		{
			for (int x = 0; x < this.x; x++)
			{
				for (int y = 0; y < this.y; y++)
				{
					for (int z = 0; z < this.z; z++)
					{
						action.Invoke(x, y, z);
					}
				}
			}
		}

		public int CompareTo(int3 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y) + this.z.CompareTo(other.z);

		public bool Equals(int3 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is int3))
				return false;
			else
				return Equals((int3)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();

		public override string ToString() => $"({X}, {Y}, {Z})";

		public static bool operator ==(int3 lhs, int3 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(int3 lhs, int3 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(int3 lhs, int3 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(int3 lhs, int3 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(int3 lhs, int3 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(int3 lhs, int3 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator int3(int[] array)
		{
			return new int3(array[0], array[1], array[2]);
		}
	}
}