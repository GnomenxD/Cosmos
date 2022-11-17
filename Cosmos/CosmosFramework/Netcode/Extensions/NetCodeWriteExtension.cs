using System;
using CosmosFramework.Netcode.Serialization;

namespace CosmosFramework
{
	internal static class NetcodeWriteExtension
	{
		public static void WriteVector2(this NetcodeWriter stream, Vector2 value)
		{
			value.X = Mathf.Round(value.X, 2, MidpointRounding.ToEven);
			value.Y = Mathf.Round(value.Y, 2, MidpointRounding.ToEven);
			stream.Write(value);
		}
	}
}