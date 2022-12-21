using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct Float2 : IComparable<Float2>, IEquatable<Float2>
	{
		private readonly float x;
		private readonly float y;

		public float X => x;
		public float Y => y;

		public float this[int index]
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

		public Float2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public int CompareTo(Float2 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y);

		public bool Equals(Float2 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Float2))
				return false;
			else
				return Equals((Float2)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

		public override string ToString() => $"({X}, {Y})";

		public static bool operator ==(Float2 lhs, Float2 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Float2 lhs, Float2 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(Float2 lhs, Float2 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(Float2 lhs, Float2 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(Float2 lhs, Float2 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(Float2 lhs, Float2 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator Float2(int[] array)
		{
			return new Float2(array[0], array[1]);
		}
	}
}