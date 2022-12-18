using System.Collections.Generic;

namespace CosmosFramework
{
	public struct Flag
	{
		private ulong value;
		public ulong Value => value;

		public Flag(ulong value)
		{
			this.value = value;
		}

		public Flag(params int[] markedPositions)
		{
			this.value = 0;
			foreach(int i in markedPositions)
			{
				Set(i);
			}
		}

		public void Set(int position)
		{
			if(position >= 64)
			{
				return;
			}
			value += (ulong)(1 << position);
		}

		public void Remove(int position)
		{
			if (position >= 64)
			{
				return;
			}
			value -= (ulong)(1 >> position);
		}

		public bool HasFlag()
		{
			return true;
		}

		public IEnumerable<int> Read()
		{
			List<int> list = new List<int>();
			ulong internalValue = value;
			int increment = (int)Mathf.Log2(internalValue);
			while (internalValue > 0)
			{
				list.Add(increment);
				Debug.Log($"Increment: {increment} | Value: {internalValue}");
				internalValue -= (uint)Mathf.Pow(2, increment);
				increment = (int)Mathf.Log2(internalValue);
			}
			list.Reverse();
			return list;
		}

		public void Clear() => value = 0;

		public string ToByteString() => System.Convert.ToString((long)value, 2);
		public override string ToString() => value.ToString();

	}
}