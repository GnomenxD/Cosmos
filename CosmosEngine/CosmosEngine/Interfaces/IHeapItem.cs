
using System;

namespace CosmosEngine.Collections
{
	public interface IHeapItem<T> : IComparable<T>
	{
		int HeapIndex { get; set; }
	}
}
