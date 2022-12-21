using System;
using CosmosFramework.Netcode.Serialization;

namespace CosmosFramework
{
	public static class NetcodeWriteExtension
	{
		public static void WriteVector2(this NetcodeWriter stream, Vector2 value)
		{
			value.X = Mathf.Round(value.X, 2, MidpointRounding.ToEven);
			value.Y = Mathf.Round(value.Y, 2, MidpointRounding.ToEven);
			stream.Write(value);
		}

		public static void WriteFloat2(this NetcodeWriter stream, Float2 value)
		{
			stream.Write(
				new Float2(
					Mathf.Round(value.X, 2, MidpointRounding.ToEven),
					Mathf.Round(value.Y, 2, MidpointRounding.ToEven)));
		}

		public static void WriteFloat3(this NetcodeWriter stream, Float3 value)
		{
			stream.Write(
				new Float3(
					Mathf.Round(value.X, 2, MidpointRounding.ToEven),
					Mathf.Round(value.Y, 2, MidpointRounding.ToEven),
					Mathf.Round(value.Z, 2, MidpointRounding.ToEven)));
		}

		public static void WriteFloat4(this NetcodeWriter stream, Float4 value)
		{
			stream.Write(
				new Float4(
					Mathf.Round(value.X, 2, MidpointRounding.ToEven),
					Mathf.Round(value.Y, 2, MidpointRounding.ToEven),
					Mathf.Round(value.Z, 2, MidpointRounding.ToEven),
					Mathf.Round(value.W, 2, MidpointRounding.ToEven)));
		}
	}
}