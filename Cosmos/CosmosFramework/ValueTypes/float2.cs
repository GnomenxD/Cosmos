﻿using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct float2 : IComparable<float2>, IEquatable<float2>
	{
		private readonly int x;
		private readonly int y;

		public int X => X;
		public int Y => Y;

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

		public float2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public int CompareTo(float2 other) => this.x.CompareTo(other.x) + this.y.CompareTo(other.y);

		public bool Equals(float2 other) => CompareTo(other) == 0;

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is float2))
				return false;
			else
				return Equals((float2)obj);
		}

		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();

		public override string ToString() => $"({X}, {Y})";

		public static bool operator ==(float2 lhs, float2 rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(float2 lhs, float2 rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator <(float2 lhs, float2 rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <=(float2 lhs, float2 rhs)
		{
			return lhs.CompareTo(rhs) <= 0;
		}

		public static bool operator >(float2 lhs, float2 rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >=(float2 lhs, float2 rhs)
		{
			return lhs.CompareTo(rhs) >= 0;
		}

		public static implicit operator float2(int[] array)
		{
			return new float2(array[0], array[1]);
		}
	}
}