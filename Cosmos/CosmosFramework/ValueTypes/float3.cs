using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct float3 : IComparable, IComparable<float3>, IEquatable<float3>
	{
		private const float Epsilon = (float)1.4e-45;
		private readonly float x;
		private readonly float y;
		private readonly float z;

		public float X => X;
		public float Y => Y;
		public float Z => Z;

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

		public float3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		int IComparable.CompareTo(object obj)
		{
			throw new NotImplementedException();
		}

		int IComparable<float3>.CompareTo(float3 other)
		{
			throw new NotImplementedException();
		}

		bool IEquatable<float3>.Equals(float3 other)
		{
			throw new NotImplementedException();
		}

		public static implicit operator float3(float[] rhs)
		{
			return new float3(rhs[0], rhs[1], rhs[2]);
		}
	}
}