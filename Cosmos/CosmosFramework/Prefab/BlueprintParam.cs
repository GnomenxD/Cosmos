using System.Collections;
using System.Collections.Generic;

namespace CosmosFramework
{
	public struct BlueprintParam : IEnumerator, IEnumerable, IEnumerable<object>
	{
		private readonly object[] param;
		private int index;

		public bool Empty => index >= param.Length;
		public object Current => param[index];

		public BlueprintParam(params object[] param)
		{
			this.param = param;
			index = -1;
		}

		/// <summary>
		/// Reads the current value in the blueprint parameters and moves the indexer to the next point.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="defaultValue">The value to be returned if the type is mismatched or cannot be retrieved.</param>
		/// <returns></returns>
		public T ReadValue<T>(T defaultValue = default)
		{
			if(param == null)
				return defaultValue;
			if (!MoveNext())
				return defaultValue;
			if (param[index].GetType() != typeof(T))
				return defaultValue;
			return (T)param[index];
		}

		public bool MoveNext()
		{
			index++;
			return (!Empty);
		}

		public void Reset()
		{
			index = -1;
		}

		public IEnumerator GetEnumerator()
		{
			return this;
		}

		IEnumerator<object> IEnumerable<object>.GetEnumerator()
		{
			return (IEnumerator<object>)GetEnumerator();
		}
	}
}