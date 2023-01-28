using Microsoft.Xna.Framework.Graphics;
using CosmosFramework.CoreModule;
using System.IO;
using System;
using Color = Microsoft.Xna.Framework.Color;
using System.CodeDom.Compiler;
using System.Net.Http;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace CosmosFramework
{
	public class Sprite : Resource
	{
		private static readonly string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
		private string contentPath;
		private SharedAssetReference assetReference;
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
				{
					if (sharedAsset)
						LoadFromSharedAsset();
					else
						Load();
				}
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

		private Sprite(string name, int library, int bufferSize, int offset)
		{
			contentPath = name;
			sharedAsset = true;
			assetReference = new SharedAssetReference(library, bufferSize, offset);
		}

		public Sprite(string contentPath)
		{
			this.contentPath = contentPath;
		}

		public Sprite(Texture2D mainTexture)
		{
			this.mainTexture = mainTexture;
			this.size = new Vector2(mainTexture.Width, mainTexture.Height);
		}

		public Texture2D Load()
		{
			if(sharedAsset)
			{
				LoadFromSharedAsset();
				return null;
			}
			return Load(contentPath);
		}

		public Texture2D Load(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				Debug.LogWarning($"Trying to load Texture2D from empty path.");
				return null;
			}
			if (!File.Exists($"{path}"))
			{
				Debug.LogWarning($"Attempting to load Texture2D from {path}, but no such file exist. Remember to copy files to output directory.");
				return null;
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
#if EDITOR
				Debug.Log($"Loaded Texture2D: {texture.Name}", LogFormat.Complete);
#endif
			}
			return texture;
		}

		private void LoadFromSharedAsset()
		{
			if (!sharedAsset)
				return;
			string sharedAssetPath = $"data/shared{assetReference.Library}.assets";
			using (StreamReader sReader = new StreamReader(sharedAssetPath))
			{
				byte[] buffer = new byte[assetReference.BufferSize];
				sReader.BaseStream.Position = assetReference.Offset;
				sReader.BaseStream.Read(buffer, 0, buffer.Length);

				using (TempFileCollection tempFile = new TempFileCollection())
				{
					string file = tempFile.AddExtension("png");
					File.WriteAllBytes(file, buffer);
					Console.WriteLine($"creating temporary file {file} for {ToString()}");
					Texture2D tex = Load(file);
					tex.Name = contentPath;
				}
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

		public override string ToString()
		{
			if (sharedAsset)
				return $"Sprite({assetReference.Library}.{contentPath} [{assetReference.BufferSize}],[{assetReference.Offset}])";
			else
				return $"Sprite({Name})";
		}

		public static Sprite Asset(string name, int library, int bufferSize, int offset)
		{
			Sprite sprite = new Sprite(name, library, bufferSize, offset);
			sprite.sharedAsset = true;

			return sprite;
		}

		public async static Task<Sprite> FromUrl(string url)
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri(url);
			var response = await client.GetAsync(url);
			if (!response.IsSuccessStatusCode)
			{
				Debug.Log($"{response.ReasonPhrase}", LogFormat.Error);
				return null;
			}
			byte[] buffer = response.Content.ReadAsByteArrayAsync().Result;
			Texture2D texture = Texture2D.FromStream(CoreModule.Core.GraphicsDeviceManager.GraphicsDevice, new MemoryStream(buffer));
			Debug.Log($"New texture from stream");
			return new Sprite(texture);
		}
	}
}
