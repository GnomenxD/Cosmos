using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Float4 : IComparable<Float4>, IEquatable<Float4>
	{
		private readonly float x;
		private readonly float y;
		private readonly float z;
		private readonly float w;

		public float X => x;
		public float Y => y;
		public float Z => z;
		public float W => w;

		public float this[int index]
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

		public Float4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public int CompareTo(Float4 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y) + this.z.CompareTo(other.z) + this.w.CompareTo(other.w);

		public bool Equals(Float4 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Float4))
				return false;
			else
				return Equals((Float4)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();

		public override string ToString() => $"({X}, {Y}, {Z}, {W})";

		public static bool operator ==(Float4 lhs, Float4 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Float4 lhs, Float4 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(Float4 lhs, Float4 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(Float4 lhs, Float4 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(Float4 lhs, Float4 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(Float4 lhs, Float4 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator Float4(float[] array)
		{
			return new Float4(array[0], array[1], array[2], array[3]);
		}
	}
}