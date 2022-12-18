
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace CosmosFramework.InputModule
{
	internal static class InputExtension
	{
		public static bool IsButtonUp(this MouseState mouse, MouseButton input)
		{
			switch(input)
			{
				case MouseButton.Left:
					return mouse.LeftButton == ButtonState.Released;
				case MouseButton.Right:
					return mouse.RightButton == ButtonState.Released;
				case MouseButton.Middle:
					return mouse.MiddleButton == ButtonState.Released;
				case MouseButton.ThumbButton1:
					return mouse.XButton1 == ButtonState.Released;
				case MouseButton.ThumbButton2:
					return mouse.XButton2 == ButtonState.Released;
			}
			return false;
		}

		public static bool IsButtonDown(this MouseState mouse, MouseButton input)
		{
			switch (input)
			{
				case MouseButton.Left:
					return mouse.LeftButton == ButtonState.Pressed;
				case MouseButton.Right:
					return mouse.RightButton == ButtonState.Pressed;
				case MouseButton.Middle:
					return mouse.MiddleButton == ButtonState.Pressed;
				case MouseButton.ThumbButton1:
					return mouse.XButton1 == ButtonState.Pressed;
				case MouseButton.ThumbButton2:
					return mouse.XButton2 == ButtonState.Pressed;
			}
			return false;
		}

		public static Microsoft.Xna.Framework.Input.Keys Convert(this CosmosFramework.InputModule.Keys value)
		{
			return (Microsoft.Xna.Framework.Input.Keys)(int)value;
		}

		public static CosmosFramework.InputModule.Keys Convert(this Microsoft.Xna.Framework.Input.Keys value)
		{
			return (CosmosFramework.InputModule.Keys)(int)value;
		}

		public static Microsoft.Xna.Framework.Input.Buttons Convert(this CosmosFramework.InputModule.GamepadButton value)
		{
			return (Microsoft.Xna.Framework.Input.Buttons)(int)value;
		}

		public static CosmosFramework.InputModule.GamepadButton Convert(this Microsoft.Xna.Framework.Input.Buttons value)
		{
			return (CosmosFramework.InputModule.GamepadButton)(int)value;
		}

		public static CosmosFramework.InputModule.Keys[] ToKeysArray(this Microsoft.Xna.Framework.Input.Keys[] values)
		{
			Keys[] keys = new Keys[values.Length];
			for (int i = 0; i < keys.Length; i++)
				keys[i] = values[i].Convert();
			return keys;
		}
	}
}
