
using Microsoft.Xna.Framework.Graphics;
using CosmosFramework.CoreModule;
using System;

namespace CosmosFramework
{
	public static class Screen
	{
		private static int previousWidth;
		private static int previousHeight;
		private static event Action onScreenSizeChanged = delegate { };
		/// <summary>
		/// Event invoked if the display window size is changed.
		/// </summary>
		public static Action OnScreenSizeChanged { get => onScreenSizeChanged; set => onScreenSizeChanged = value; }

		/// <summary>
		/// Get or set the application window width.
		/// </summary>
		public static int Width
		{
			get => Core.ViewportSize.X;
			set => Core.Instance.SetResolution(value, Height, IsFullScreen);
		}


		/// <summary>
		/// Get or set the application window height.
		/// </summary>
		public static int Height
		{
			get => Core.ViewportSize.Y;
			set => Core.Instance.SetResolution(Width, value, IsFullScreen);
		}
		/// <summary>
		/// Returns the pixel centre of the screen.
		/// </summary>
		public static Vector2 Centre => new Vector2(Width / 2, Height / 2);
		/// <summary>
		/// Returns the ratio of the screen Width / Height.
		/// </summary>
		public static float Ratio => (float)Screen.Width / (float)Screen.Height;

		/// <summary>
		/// Returns the screen width.
		/// </summary>
		public static int DisplayWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
		/// <summary>
		/// Returns the screen height.
		/// </summary>
		public static int DisplayHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
		public static Vector2Int Resolution
		{
			get => new Vector2Int(Width, Height);
			set => Core.Instance.SetResolution(value.X, value.Y, IsFullScreen);
		}
		public static bool IsFullScreen
		{
			get => Core.IsFullScreen;
			set => Core.Instance.SetResolution(Width, Height, value);
		}
		/// <summary>
		/// Changes fullscreen mode
		/// </summary>
		public static void ChangeFullScreenMode()
		{
			previousWidth = Width;
			previousHeight = Height;
			if (IsFullScreen)
			{
				SetResolution(previousWidth, previousHeight, false);
			}
			else
			{
				SetResolution(DisplayWidth, DisplayHeight, true);
			}
		}
		/// <summary>
		/// Sets the resolution of the game window to the <paramref name="width"/> and <paramref name="height"/>.
		/// </summary>
		public static void SetResolution(int width, int height, bool fullscreen = true) => Core.Instance.SetResolution(width, height, fullscreen);
		/// <summary>
		/// Sets the resolution of the game window to the 16:9 predetermined resolution.
		/// </summary>
		public static void SetResolution(ScreenResolution resolution, bool fullscreen = true) => Core.Instance.SetResolution(resolution, fullscreen);
	}
}
