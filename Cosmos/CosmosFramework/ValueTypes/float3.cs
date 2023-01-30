using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Float3 : IComparable<Float3>, IEquatable<Float3>
	{
		private readonly float x;
		private readonly float y;
		private readonly float z;

		public float X => x;
		public float Y => y;
		public float Z => z;

		public float this[int index]
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

		public Float3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public int CompareTo(Float3 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y) + this.z.CompareTo(other.z);

		public bool Equals(Float3 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Float3))
				return false;
			else
				return Equals((Float3)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();

		public override string ToString() => $"({X}, {Y}, {Z})";

		public static bool operator ==(Float3 lhs, Float3 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Float3 lhs, Float3 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(Float3 lhs, Float3 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(Float3 lhs, Float3 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(Float3 lhs, Float3 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(Float3 lhs, Float3 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator Float3(float[] array)
		{
			return new Float3(array[0], array[1], array[2]);
		}
	}
}