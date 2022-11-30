using System.Runtime.InteropServices;
using System;

namespace CosmosFramework
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public readonly struct int3 : IComparable, IComparable<int3>, IEquatable<int3>
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

		int IComparable.CompareTo(object obj)
		{
			throw new NotImplementedException();
		}

		int IComparable<int3>.CompareTo(int3 other)
		{
			throw new NotImplementedException();
		}

		bool IEquatable<int3>.Equals(int3 other)
		{
			throw new NotImplementedException();
		}

		public static implicit operator int3(int[] rhs)
		{
			return new int3(rhs[0], rhs[1], rhs[2]);
		}
	}
}