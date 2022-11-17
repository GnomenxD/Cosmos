namespace CosmosFramework
{
	public static class MinMaxExtension
	{
		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Mathf.ClampBetween(int, int, int)"/>
		/// </summary>
		public static int Clamp(this MinMaxInt minMax, int value) => Mathf.ClampBetween(minMax.Min, minMax.Max, value);
		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Mathf.ClampBetween(int, int, int)"/>
		/// </summary>
		public static float Clamp(this MinMaxFloat minMax, float value) => Mathf.ClampBetween(minMax.Min, minMax.Max, value);

		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Random.Range(int, int)"/>
		/// </summary>
		public static int RandomInRange(this MinMaxInt minMax) => Random.Range(minMax.Min, minMax.Max);
		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Random.Range(float, float)"/>
		/// </summary>
		public static float RandomInRange(this MinMaxFloat minMax) => Random.Range(minMax.Min, minMax.Max);

		/// <summary>
		/// Returns true if <paramref name="value"/> is between min and max.
		/// </summary>
		public static bool IsInRange(this MinMaxInt minMax, int value) => value >= minMax.Min && value <= minMax.Max;
		/// <summary>
		/// Returns true if <paramref name="value"/> is between min and max.
		/// </summary>
		public static bool IsInRange(this MinMaxFloat minMax, float value) => value >= minMax.Min && value <= minMax.Max;

		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Mathf.Lerp(float, float, float)"/>
		/// </summary>
		public static float Lerp(this MinMaxInt minMax, float t) => Mathf.Lerp(minMax.Min, minMax.Max, t);
		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Mathf.Lerp(float, float, float)"/>
		/// </summary>
		public static float Lerp(this MinMaxFloat minMax, float t) => Mathf.Lerp(minMax.Min, minMax.Max, t);

		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Mathf.LerpUnclamped(float, float, float)"/>
		/// </summary>
		public static float LerpUnclamped(this MinMaxInt minMax, float t) => Mathf.LerpUnclamped(minMax.Min, minMax.Max, t);
		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Mathf.LerpUnclamped(float, float, float)"/>
		/// </summary>
		public static float LerpUnclamped(this MinMaxFloat minMax, float t) => Mathf.LerpUnclamped(minMax.Min, minMax.Max, t);

	}
}