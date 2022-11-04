using CosmosEngine;

namespace SpaceWar
{
	public static class HexMetrics
	{
		public const float sign = 0.866025404f;
		public const float outerRadius = 1f;
		public const float innerRadius = outerRadius * sign; //0.866025404f;
		public static float sin60 = Mathf.Sqrt(3f) * 0.5f;

		//Flat top orientation
		public static Vector2[] corners =
		{
			new Vector2(-0.5f * outerRadius, innerRadius),
			new Vector2(0.5f * outerRadius, innerRadius),
			new Vector2(outerRadius, 0f),
			new Vector2(0.5f * outerRadius, -innerRadius),
			new Vector2(-0.5f * outerRadius,  -innerRadius),
			new Vector2(-outerRadius, 0f),
			new Vector2(-0.5f * outerRadius, innerRadius),
		};

		//Pointy top orientation
		//public static Vector2[] corners = 
		//{
		//	new Vector2(0f, outerRadius),
		//	new Vector2(innerRadius, 0.5f * outerRadius),
		//	new Vector2(innerRadius, -0.5f * outerRadius),
		//	new Vector2(0f, -outerRadius),
		//	new Vector2(-innerRadius, -0.5f * outerRadius),
		//	new Vector2(-innerRadius, 0.5f * outerRadius),
		//	new Vector2(0f, outerRadius),
		//};


	}
}