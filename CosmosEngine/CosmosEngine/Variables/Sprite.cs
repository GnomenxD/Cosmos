﻿using Microsoft.Xna.Framework.Graphics;
using CosmosEngine.CoreModule;
using System.IO;
using System;
using Color = Microsoft.Xna.Framework.Color;
using System.CodeDom.Compiler;

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
		private bool sharedAsset;
		private event Action spriteContentModifiedEvent;

		public string Name => (mainTexture != null ? mainTexture.Name : string.IsNullOrWhiteSpace(contentPath) ? "null" : contentPath);
		public string FullPath => $"{AppDomain.CurrentDomain.BaseDirectory}/{contentPath}";

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

		public Sprite()
		{

		}

		private Sprite(string name, int library, int buffer, int offset)
		{

		}

		public Sprite(string contentPath)
		{
			this.contentPath = contentPath;
		}

		public Sprite(Texture2D mainTexture)
		{
			this.mainTexture = mainTexture;
		}

		private void LoadFromSharedAsset()
		{

		}

		public void Load() => Load(contentPath);

		public void Load(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				Debug.LogWarning($"Trying to load Texture2D from empty path.");
				return;
			}
			if (!File.Exists($"{path}"))
			{
				Debug.LogWarning($"Attempting to load Texture2D from {path}, but no such file exist. Remember to copy files to output directory.");
				return;
			}

			Texture2D texture = null;
			using (FileStream stream = new FileStream($"{path}", FileMode.Open))
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

		protected override void Dispose(bool disposing)
		{
			if(!IsDisposed && disposing)
			{
				mainTexture.Dispose();
			}
			base.Dispose(disposing);
		}

		public override string ToString() => $"Sprite({Name})";

		public static Sprite Asset(string name, int library, int buffer, int offset)
		{
			Sprite sprite = new Sprite(name, library, buffer, offset);
			sprite.sharedAsset = true;

			return sprite;
		}
	}
}
