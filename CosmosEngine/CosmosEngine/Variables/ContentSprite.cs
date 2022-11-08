using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace CosmosEngine.Variables
{
	public class ContentSprite
	{
		private Texture2D mainTexture;
		private string path;

		public Texture2D Texture => mainTexture ??= Load();

		public ContentSprite(string path)
		{
			this.path = path;
		}

		public Texture2D Load()
		{
			Texture2D texture;
			using (FileStream stream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}/Assets/{path}.png", FileMode.Open))
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