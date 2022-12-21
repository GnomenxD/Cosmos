using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CosmosFramework
{
	[System.Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public struct Flag
	{
		private ulong packedValue;
		public ulong PackedValue => packedValue;

		public Flag(ulong value)
		{
			this.packedValue = value;
		}

		public Flag(params int[] markedPositions)
		{
			this.packedValue = 0;
			foreach (int i in markedPositions)
			{
				Mark(i);
			}
		}

		public void Mark(int position)
		{
			if (position >= 64)
			{
				return;
			}
			packedValue += (ulong)(1 << position);
		}

		public void Remove(int position)
		{
			if (position >= 64)
			{
				return;
			}
			packedValue -= (ulong)(1 >> position);
		}

		public bool HasFlag()
		{
			return true;
		}

		public IEnumerable<int> Iterate()
		{
			List<int> list = new List<int>();
			ulong internalValue = packedValue;
			int increment = (int)Mathf.Log2(internalValue);
			while (internalValue > 0)
			{
				list.Add(increment);
				//Debug.Log($"Increment: {increment} | Value: {internalValue}");
				internalValue -= (uint)Mathf.Pow(2, increment);
				increment = (int)Mathf.Log2(internalValue);
			}
			list.Reverse();
			return list;
		}

		public void Clear() => packedValue = 0;

		public string ToByteString() => System.Convert.ToString((long)packedValue, 2);
		public override string ToString() => packedValue.ToString();

		public static implicit operator ulong(Flag flag) => flag.packedValue;
	}
}