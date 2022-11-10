﻿using Microsoft.Xna.Framework.Graphics;
using CosmosEngine.CoreModule;
using System.IO;
using System;
using Color = Microsoft.Xna.Framework.Color;

namespace CosmosEngine
{
	public class Sprite : Resource
	{
		private static readonly string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
		private string contentPath;
		private Texture2D mainTexture;
		private Vector2 size;
		//Pivot
		//LoadingMode
		//SpriteMode
		//WrapMode
		//FilterMode
		private int pixelsPerUnit = 100;
		private event Action spriteContentModifiedEvent;

		public string Name => (mainTexture != null ? mainTexture.Name : string.IsNullOrWhiteSpace(contentPath) ? "null" : contentPath);
		public Texture2D Texture
		{
			get
			{
				if(mainTexture == null)
					Load();
				return mainTexture;
			}
		}
		public Vector2 Size
		{
			get
			{
				if (mainTexture == null)
					Load();
				return size;
			}
		}
		public int Width => (int)Size.X;
		public int Height => (int)Size.Y;
		public int PixelsPerUnit => pixelsPerUnit;
		public Action SpriteContentModified { get => spriteContentModifiedEvent; set => spriteContentModifiedEvent = value; }


		public Sprite(string contentPath)
		{
			this.contentPath = contentPath;
		}


		public Sprite(Texture2D mainTexture)
		{
			this.mainTexture = mainTexture;
		}

		public void Load() => Load(contentPath);

		public void Load(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				Debug.LogWarning($"Trying to load Texture2D from empty path.");
				return;
			}
			if (!File.Exists($"{rootDirectory}/{path}"))
			{
				Debug.LogWarning($"Attempting to load Texture2D from {path}, but no such file exist. Remember to copy files to output directory.");
				return;
			}

			Texture2D texture = null;
			using (FileStream stream = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}/{path}", FileMode.Open))
			{
				texture = Texture2D.FromStream(CoreModule.Core.GraphicsDeviceManager.GraphicsDevice, stream);
			};

			if(texture != null)
			{
				Color[] buffer = new Color[texture.Width * texture.Height];
				texture.GetData(buffer);
				for (int i = 0; i < buffer.Length; i++)
				{
					buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
				}
				texture.SetData(buffer);

				texture.Name = path;
				AssignTexture(texture);
				Debug.Log($"Loaded Texture2D: {texture.Name}", LogFormat.Complete);
			}
		}

		private void AssignTexture(Texture2D texture)
		{
			mainTexture = texture;
			size = new Vector2(texture.Width, texture.Height);
			SpriteContentModified?.Invoke();
		}

		public Rect GetSpriteRect()
		{
			return new Rect(0, 0, Width, Height);
		}

		public Texture2D? Load()
		{
			Debug.Log("LOAD " + path + $"My texture: {(mainTexture == null ? "null" : mainTexture)}");
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
				Debug.Log($"Loaded Texture {path} -- {textureSize}");
			}
			return texture;
		}

		public Texture2D LoadThroughContentManager()
		{
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}
			Texture2D texture = ContentManager.Load<Texture2D>(path);

			if (texture != null)
			{
				this.mainTexture = texture;
				this.textureSize = new Vector2(texture.Width, texture.Height);
				this.Pivot = new Vector2(0.5f, 0.5f);
			}

			Debug.Log($"Size: {textureSize}");
			Debug.Log($"Loaded Texture {path} through the ContentManager");
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

		public override string ToString() => $"Sprite({Name})";
	}
}
