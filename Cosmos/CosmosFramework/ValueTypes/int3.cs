using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Int3 : IComparable<Int3>, IEquatable<Int3>
	{
		private readonly int x;
		private readonly int y;
		private readonly int z;

		public int X => x;
		public int Y => y;
		public int Z => z;

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

		public Int3(int x, int y, int z)
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

		public int CompareTo(Int3 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y) + this.z.CompareTo(other.z);

		public bool Equals(Int3 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Int3))
				return false;
			else
				return Equals((Int3)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();

		public override string ToString() => $"({X}, {Y}, {Z})";

		public static bool operator ==(Int3 lhs, Int3 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Int3 lhs, Int3 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(Int3 lhs, Int3 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(Int3 lhs, Int3 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(Int3 lhs, Int3 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(Int3 lhs, Int3 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator Int3(int[] array)
		{
			return new Int3(array[0], array[1], array[2]);
		}
	}
}