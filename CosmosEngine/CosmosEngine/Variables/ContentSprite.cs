using CosmosEngine.CoreModule;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace CosmosEngine
{
	public class ContentSprite : Resource
	{
		private Texture2D mainTexture;
		private string path;

		public Texture2D Texture => mainTexture ??= Load();

		~ContentSprite() => mainTexture?.Dispose();

		public ContentSprite(string path)
		{
			this.path = path;
		}

		public Texture2D Load()
		{
			Texture2D texture;
			if(!File.Exists(path))
			{
				Debug.LogError($"Trying to load Sprite from {path} but no such file exist. Remember to Copy to Output Directory");
				return DefaultGeometry.Square.Texture;
			}
			using (FileStream stream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}/{path}", FileMode.Open))
			{
				texture = Texture2D.FromStream(CoreModule.Core.GraphicsDeviceManager.GraphicsDevice, stream);
			};
			if(texture != null)
			{
				Color[] buffer = new Color[texture.Width * texture.Height];
				texture.GetData(buffer);
				for(int i = 0; i < buffer.Length; i++)
				{
					buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
				}
				texture.SetData(buffer);
			}
			return texture;
		}
		public static explicit operator Sprite(ContentSprite sprite) => new Sprite(sprite.Texture);
	}
}