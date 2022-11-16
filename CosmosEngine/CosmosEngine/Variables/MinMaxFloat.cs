
namespace CosmosEngine
{
	[System.Serializable]
	public struct MinMaxFloat
	{
		private float min;
		private float max;

		/// <summary>
		/// Minimum value.
		/// </summary>
		public float Min { get => min; set => min = value; }
		/// <summary>
		/// Maximum value.
		/// </summary>
		public float Max { get => max; set => max = value; }

		public MinMaxFloat(float min, float max)
		{
			this.min = min;
			this.max = max;
			if (min >= max)
			{
				Debug.Log($"{GetType().FullName}, min value ({min}) cannot be greater or equals to max value ({max}) - max will be increased to {min + 1}.", LogFormat.Warning);
				this.max = min + 1;
			}
		}

		public float Evaluate(float value) => Mathf.Clamp01(1 - (value - max) / (min - max));
	}
}