using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Int4 : IComparable<Int4>, IEquatable<Int4>
	{
		private readonly int x;
		private readonly int y;
		private readonly int z;
		private readonly int w;

		public int X => x;
		public int Y => y;
		public int Z => z;
		public int W => w;

		public int this[int index]
		{
			get
			{
				return index switch
				{
					0 => X,
					1 => Y,
					2 => Z,
					3 => W,
					_ => throw new IndexOutOfRangeException(),
				};
			}
		}

		public Int4(int x, int y, int z, int w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public int GetLength(int index) => index switch
		{
			0 => X,
			1 => Y,
			2 => Z,
			3 => W,
			_ => throw new IndexOutOfRangeException(),
		};

		public void For(Action<int, int, int, int> action)
		{
			for (int x = 0; x < this.x; x++)
			{
				for (int y = 0; y < this.y; y++)
				{
					for (int z = 0; z < this.z; z++)
					{
						for (int w = 0; w < this.w; w++)
						{
							action.Invoke(x, y, z, w);
						}
					}
				}
			}
		}

		public int CompareTo(Int4 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y) + this.z.CompareTo(other.z) + this.w.CompareTo(other.w);

		public bool Equals(Int4 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Int4))
				return false;
			else
				return Equals((Int4)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^w.GetHashCode();

		public override string ToString() => $"({X}, {Y}, {Z}, {W})";

		public static bool operator ==(Int4 lhs, Int4 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Int4 lhs, Int4 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(Int4 lhs, Int4 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(Int4 lhs, Int4 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(Int4 lhs, Int4 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(Int4 lhs, Int4 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator Int4(int[] array)
		{
			return new Int4(array[0], array[1], array[2], array[3]);
		}
	}
}