using System.CodeDom.Compiler;
using System.IO;
using System;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Color = Microsoft.Xna.Framework.Color;

#nullable enable
namespace CosmosFramework
{
	public static class TextureExtensions
	{
		public static Texture2D? LoadFromSharedAssets(this Texture2D? texture, int library, int bufferSize, int offset)
		{
			string sharedAssetPath = $"data/shared{library}.assets";
			if (!File.Exists(sharedAssetPath))
			{
				Debug.Log($"Failed to locate sharedassets", LogFormat.Error);
				return null;
			}
			using (StreamReader sReader = new StreamReader(sharedAssetPath))
			{
				byte[] buffer = new byte[bufferSize];
				sReader.BaseStream.Position = offset;
				sReader.BaseStream.Read(buffer, 0, buffer.Length);

				using (TempFileCollection tempFile = new TempFileCollection())
				{
					string file = tempFile.AddExtension("png");
					File.WriteAllBytes(file, buffer);
					Debug.Log($"Created temporary file: {file}");
					Console.WriteLine($"creating temporary file {file}");
					using (FileStream stream = new FileStream($"{file}", FileMode.Open))
					{
						texture = Texture2D.FromStream(CoreModule.Core.GraphicsDeviceManager.GraphicsDevice, stream);
					};

					Debug.Log(texture == null);
					if (texture != null)
					{
						Color[] data = new Color[texture.Width * texture.Height];
						texture.GetData(data);
						for (int i = 0; i < data.Length; i++)
						{
							data[i] = Color.FromNonPremultiplied(data[i].R, data[i].G, data[i].B, data[i].A);
						}
						texture.SetData(data);
					}
				}
				return texture;
			}
		}
	}
}