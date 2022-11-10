using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CosmosEngine.CoreModule;
using System.IO;
using System;
using Color = Microsoft.Xna.Framework.Color;

namespace CosmosEngine
{
	public class Sprite : Resource
	{
		private Vector2 pivot;
		private Vector2Int origin;
		private string path;
		private Texture2D mainTexture;
		private Vector2 textureSize;
		private readonly LoadingMode loadingMode;
		private readonly SpriteMode spriteMode;
		private readonly WrapMode wrapMode;
		private readonly FilterMode filterMode;
		private int pixelsPerUnit;

		private static ContentManager ContentManager => Core.Instance.Content;

		/// <summary>
		/// Get the reference to the used texture.
		/// </summary>
		public Texture2D Texture => mainTexture ??= Load();
		public Vector2 Pivot 
		{ 
			get => pivot;
			set
			{
				pivot = value;
				origin = new Vector2Int(Mathf.RoundToInt(Width * pivot.X), Mathf.RoundToInt(Height * pivot.Y));
			}
		}
		public Vector2 Origin => (Vector2)origin;
		public LoadingMode LoadingMode => loadingMode;
		/// <summary>
		/// Sprite texture import mode.
		/// </summary>
		public SpriteMode SpriteMode => spriteMode;
		/// <summary>
		/// Texture coordinate wrapping mode.
		/// </summary>
		public WrapMode WrapMode => wrapMode;
		/// <summary>
		/// Filtering mode of the Texture.
		/// </summary>
		public FilterMode FilterMode => filterMode;
		/// <summary>
		/// The number of pixels in the sprite that correspond to one unit in world space.
		/// </summary>
		public int PixelsPerUnit { get => pixelsPerUnit; set => pixelsPerUnit = value; }
		public Vector2Int Size => (Vector2Int)textureSize;
		public int Width => Size.X;
		public int Height => Size.Y;

		/// <summary>
		/// Generate an empty sprite.
		/// </summary>
		public Sprite() : this(string.Empty, LoadingMode.LazyLoading, SpriteMode.Single, WrapMode.Clamped, FilterMode.Linear, 100)
		{
		}

		/// <summary>
		/// Generate a sprite, loading the texture at the given path.
		/// </summary>
		public Sprite(string path) : this(path, LoadingMode.LazyLoading, SpriteMode.Single, WrapMode.Clamped, FilterMode.Linear, 100)
		{
		}

		/// <summary>
		/// Generate a sprite with a given texture.
		/// </summary>
		public Sprite(Texture2D texture) : this(texture, SpriteMode.Single, WrapMode.Clamped, FilterMode.Linear, 100)
		{
		}

		/// <summary>
		/// Generate a sprite, loading the texture at the given path.
		/// </summary>
		public Sprite(string path, LoadingMode loadingMode = LoadingMode.LazyLoading, SpriteMode spriteMode = SpriteMode.Single, WrapMode wrapMode = WrapMode.Clamped, FilterMode filterMode = FilterMode.Linear, int pixelsPerUnit = 100)
		{
			this.loadingMode = loadingMode;
			if (!string.IsNullOrEmpty(path))
			{
				this.path = path;
				if (loadingMode == LoadingMode.EagerLoading)
					mainTexture = Load();
			}
			this.spriteMode = spriteMode;
			this.wrapMode = wrapMode;
			this.filterMode = filterMode;
			this.pixelsPerUnit = pixelsPerUnit;
		}

		/// <summary>
		/// Generate a sprite with a given texture.
		/// </summary>
		public Sprite(Texture2D texture, SpriteMode spriteMode = SpriteMode.Single, WrapMode wrapMode = WrapMode.Clamped, FilterMode filterMode = FilterMode.Linear, int pixelsPerUnit = 100)
		{
			this.mainTexture = texture;
			this.textureSize = new Vector2(texture.Width, texture.Height);
			this.Pivot = new Vector2(0.5f, 0.5f);
			this.spriteMode = spriteMode;
			this.wrapMode = wrapMode;
			this.filterMode = filterMode;
			this.pixelsPerUnit = pixelsPerUnit;
		}

		public Texture2D Load()
		{
			Texture2D texture;
			if (!File.Exists(path))
			{
				Debug.LogError($"Trying to load Sprite from {path} but no such file exist. Remember to Copy to Output Directory");
				return DefaultGeometry.Square.Texture;
			}
			using (FileStream stream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}/{path}", FileMode.Open))
			{
				texture = Texture2D.FromStream(CoreModule.Core.GraphicsDeviceManager.GraphicsDevice, stream);
			};
			if (texture != null)
			{
				Color[] buffer = new Color[texture.Width * texture.Height];
				texture.GetData(buffer);
				for (int i = 0; i < buffer.Length; i++)
				{
					buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
				}
				texture.SetData(buffer);

				textureSize = new Vector2(texture.Width, texture.Height);
				Pivot = new Vector2(0.5f, 0.5f);
			}
			return texture;
		}

		public Texture2D LoadThroughContentManager()
		{
			if (!string.IsNullOrEmpty(path))
			{
				return null;
			}
			Texture2D texture = ContentManager.Load<Texture2D>(path);

			this.mainTexture = texture;
			this.textureSize = new Vector2(texture.Width, texture.Height);
			this.Pivot = new Vector2(0.5f, 0.5f);

			return texture;
		}

		public void Clear() => mainTexture = null;

		protected override void Dispose(bool disposing)
		{
			if(!IsDisposed && disposing)
			{
				mainTexture.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
