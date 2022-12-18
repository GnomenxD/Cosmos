using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct float4 : IComparable<float4>, IEquatable<float4>
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

		public float4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public int CompareTo(float4 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y) + this.z.CompareTo(other.z) + this.w.CompareTo(other.w);

		public bool Equals(float4 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is float4))
				return false;
			else
				return Equals((float4)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();

		public override string ToString() => $"({X}, {Y}, {Z}, {W})";

		public static bool operator ==(float4 lhs, float4 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(float4 lhs, float4 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(float4 lhs, float4 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(float4 lhs, float4 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(float4 lhs, float4 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(float4 lhs, float4 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator float4(float[] array)
		{
			return new float4(array[0], array[1], array[2], array[3]);
		}
	}
}