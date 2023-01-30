
using System;

namespace CosmosFramework
{
	[System.Serializable]
	public struct MinMaxInt
	{
		private int min;
		private int max;

		/// <summary>
		/// Minimum value.
		/// </summary>
		public int Min { get => min; set => min = value; }
		/// <summary>
		/// Maximum value.
		/// </summary>
		public int Max { get => max; set => max = value; }

		public MinMaxInt(int min, int max)
		{
			this.min = min;
			this.max = max;
			if (min >= max)
			{
				Debug.Log($"{GetType().FullName}, min value ({min}) cannot be greater or equals to max value ({max}) - max will be increased to {min + 1}.", LogFormat.Warning);
				this.max = min + 1;
			}
		}

		/// <summary>
		/// Iterates from the min value to max value of the <see cref="CosmosEngine.MinMaxInt"/>, performing given <paramref name="action"/> each iteration.
		/// </summary>
		/// <param name="action">The performed action each iteration.</param>
		/// <param name="inclusive">Wether the iteration is inclusive of the max value or exclusive of the max value.</param>
		public void For(Action action, bool inclusive= false)
		{
			if (inclusive)
			{
				for (int i = min; i <= max; i++)
					action.Invoke();
			}
			else
			{
				for (int i = min; i < max; i++)
					action.Invoke();
			}
		}

		/// <summary>
		/// Iterates from the <see cref="CosmosEngine.MinMaxInt"/> min value to max value, performing given <paramref name="action"/> each iteration.
		/// </summary>
		/// <param name="action">The performed action each iteration, will pass the an index as parameter.</param>
		/// <param name="inclusive">Wether the iteration is inclusive of the max value or exclusive of the max value.</param>
		public void For(Action<int> action, bool inclusive = false)
		{
			if (inclusive)
			{
				for (int i = min; i <= max; i++)
					action.Invoke(i);
			}
			else
			{
				for (int i = min; i < max; i++)
					action.Invoke(i);
			}
		}
	}
}
