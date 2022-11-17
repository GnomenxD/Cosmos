using System.IO;
using System.Text;

namespace AssetLibraryBuilder
{
	internal class LibraryBuilder
	{
		private const int bufferMaxSize = 268435456;
		private string? assetFolder;
		private string? binFolder;
		private string? outputDirectory;
		private string rootDirectory;
		private string configurationName;
		private StringBuilder stringBuilder;

		public LibraryBuilder(string rootDirectory, string configurationName)
		{
			this.rootDirectory = rootDirectory;
			this.configurationName = configurationName;
			stringBuilder = new StringBuilder();
		}

		public void CreateLibrary()
		{
			System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
			Console.WriteLine($"------ Creationg Asset Library");
			if (!LocateAssetDirectory())
				return;
			if (!LocateOutputDirectory())
				return;

			Console.WriteLine($"root directory: {rootDirectory}");
			Console.WriteLine($"output directory: {outputDirectory}");

			if (assetFolder == null)
			{
				Console.WriteLine(ErrorCode.NoAssetDirectory(null));
				return;
			}
			string libraryPath = CreateAssetLibraryFolder();
			GenerateSpriteLibrary(libraryPath);
			GenerateAudioLibrary(libraryPath);

			stopwatch.Stop();
			Console.WriteLine($"------ Asset Library was successful constructed [{stopwatch.Elapsed.TotalSeconds:F2}]");
		}

		#region Folder Structure

		private bool LocateAssetDirectory()
		{
			LocateFolder(rootDirectory, "Assets", ref assetFolder, new Func<string, bool>[]
			{
				(s) => s.StartsWith("."),
				(s) => s.Equals("bin", StringComparison.CurrentCultureIgnoreCase),
				(s) => s.Equals("obj", StringComparison.CurrentCultureIgnoreCase),
			});

			if (assetFolder == null)
			{
				Console.WriteLine(ErrorCode.NoOutputDirectory(null));
				return false;
			}

			return true;
		}

		private bool LocateOutputDirectory()
		{
			//First we locate the bin folder.
			LocateFolder(rootDirectory, "bin", ref binFolder, new Func<string, bool>[]
			{
				(s) => s.StartsWith("."),
				(s) => s.Equals("Assets"),
				(s) => s.Equals("Editor"),
				(s) => s.Equals("Content", StringComparison.CurrentCultureIgnoreCase),
			});

			if(binFolder == null)
			{
				Console.WriteLine(ErrorCode.NoOutputDirectory(null));
				return false;
			}

			//Next we find the corresponding configuration folder to output to.
			string? configurationFolder = null;
			LocateFolder(binFolder, configurationName, ref configurationFolder, new Func<string, bool>[]
			{
				(s) => s.StartsWith("."),
				(s) => s.Equals("Assets"),
				(s) => s.Equals("Content", StringComparison.CurrentCultureIgnoreCase),
			});

			if (configurationFolder == null)
			{
				Console.WriteLine(ErrorCode.MismatchConfigurationDirectory(null));
				return false;
			}

			//Last we need to find the actually output folder, the one which contains the .exe file.
			//This is not optimal and should be 
			string? outputFolder = null;
			LocateFolder(configurationFolder, "runtimes", ref outputFolder, new Func<string, bool>[]
			{
				(s) => s.StartsWith("."),
				(s) => s.Equals("Assets"),
				(s) => s.Equals("Content", StringComparison.CurrentCultureIgnoreCase),
			});

			if (outputFolder == null)
			{
				Console.WriteLine(ErrorCode.NoOutputDirectory(null));
				return false;
			}

			outputDirectory = $"{outputFolder.Replace("runtimes", "data")}";
			Directory.CreateDirectory(outputDirectory);
			string[] existingAssetFiles = LoadAllAssets(outputDirectory, "*.assets");
			foreach (string s in existingAssetFiles)
				File.Delete(s);
			return true;
		}

		private string CreateAssetLibraryFolder()
		{
			string libraryPath = Directory.GetParent(assetFolder).FullName + $"\\AssetsLibrary";
			Directory.CreateDirectory(libraryPath);
			return libraryPath;
		}

		#endregion

		#region Library Generation

		/*using CosmosEngine;
		 * 
		 * public static partical class Assets
		 * {
		 * #region Sprites
		 * 
		 * #if subfolder.length > 0
		 * public static partical class Subfolder
		 * {
		 *		public static Sprite AssetName { get; } = Sprite.Asset(name, library, buffer.Length, offset);
		 * }
		 * 
		 * public static Sprite AssetName { get; } = Sprite.Asset(name, library, buffer.Length, offset);
		 * 
		 * #endregion
		 * }
		 */

		public void GenerateSpriteLibrary(string libraryPath)
		{
			List<SpriteAssetReference> spriteAssets = GenerateSpriteAssetReferences();
			GenerateSharedAssets(spriteAssets);

			if(!File.Exists($"{libraryPath}\\Assets.cs"))
			{
				Console.WriteLine(ErrorCode.AssetsFileMissing($"{libraryPath}\\Assets.cs"));
			}

			int scriptLine = 6;
			StringBuilder sb = new StringBuilder();
			using (StreamWriter writer = new StreamWriter($"{libraryPath}\\Assets.cs"))
			{
				sb.AppendLine("using CosmosFramework;\n");
				sb.AppendLine("public static partial class Assets");
				sb.AppendLine("{");

				sb.AppendLine("\t#region Sprites");

				foreach (SpriteAssetReference assetReference in spriteAssets)
				{
					sb.AppendLine(CreateSpriteProperty(assetReference, scriptLine));
					scriptLine += 2;
				}

				sb.AppendLine("\t#endregion");

				sb.AppendLine("}");

				writer.Write(sb.Normalize());
			}
			sb.Clear();
			Console.WriteLine($"libraryPath: {libraryPath}");

			GenerateSharedAssets(spriteAssets);
		}

		private List<SpriteAssetReference> GenerateSpriteAssetReferences()
		{
			List<SpriteAssetReference> spriteAssets = new List<SpriteAssetReference>();
			List<string> assetPaths = new List<string>();
			LoadAllAssets(assetPaths, $"{assetFolder}\\Sprites", "*.png", "*.jpg");

			foreach(string assetPath in assetPaths)
			{
				SpriteAssetReference? assetReference = CreateSpriteAsset(assetPath);
				if (assetReference == null)
					continue;
				if(spriteAssets.Exists(item => item.CompareAssetLink(assetReference)))
				{
					//Console.WriteLine(ErrorCode.DuplicateSprite(assetReference));
					continue;
				}
				spriteAssets.Add(assetReference);
			}
			return spriteAssets;
		}

		private int GenerateSharedAssets(List<SpriteAssetReference> spriteAssets)
		{
			int offset = 0;
			int library = 0;
			int assetIndex = 0;
			int maxSize = bufferMaxSize;

		sharedAssetCreation:
			FileStream stream = File.Open($"{outputDirectory}\\shared{library}.assets", FileMode.Create);
			StreamWriter writer = new StreamWriter(stream);
			for (int i = assetIndex; i < spriteAssets.Count; i++)
			{
				SpriteAssetReference asset = spriteAssets[i];
				assetIndex++;
				writer.BaseStream.Write(asset.Buffer, 0, asset.Buffer.Length);
				asset.Library = library;
				asset.Offset = offset;
				offset += asset.Buffer.Length;
				if (offset + asset.Buffer.Length >= maxSize)
				{
					library++;
					offset = 0;
					writer.Close();
					stream.Close();
					goto sharedAssetCreation;
				}
			}
			writer.Close();
			stream.Close();
			Console.WriteLine($"generated {library + 1} shared.assets file{(library >= 1 ? "s" : "")} at {outputDirectory}");

			//Compressing went from 73.116KB to around 65.600KB - 65.178KB, which is a 10-11% decrease in space usage. Could be looked into later.
			//To do this Comos Framework needs to handle decompression. Tests below
				//byte[] buffer = File.ReadAllBytes($"{outputDirectory}\\shared0.assets");
				//using (FileStream fcStream = File.Open($"{outputDirectory}\\fastCompressed_shared{library}.assets", FileMode.Create))
				//	buffer.Compress(fcStream, System.IO.Compression.CompressionLevel.Fastest);
				//using (FileStream ocStream = File.Open($"{outputDirectory}\\optimalCompressed_shared{library}.assets", FileMode.Create))
				//	buffer.Compress(ocStream, System.IO.Compression.CompressionLevel.Optimal);
				//using (FileStream sscStream = File.Open($"{outputDirectory}\\smallCompressed_shared{library}.assets", FileMode.Create))
				//	buffer.Compress(sscStream, System.IO.Compression.CompressionLevel.SmallestSize);

			return library;
		}

		private SpriteAssetReference? CreateSpriteAsset(string assetPath)
		{
			string[] assetSplit = assetPath.Split('\\');
			//Get the sprite name from the full path.
			string assetName = assetSplit[assetSplit.Length - 1];
			assetName = assetName
				.Replace("(", "")
				.Replace(")", "")
				.Replace("/", "")
				.Replace("@", "")
				.Replace("\\", "");

			//Generate byte array from file.
			byte[] buffer = File.ReadAllBytes(assetPath);

			//Generate a Subfolder link to make the asset library folder more accessable when having a lot of files.
			List<string> subFolder = new List<string>();
			for (int i = assetSplit.Length - 1; i >= 0; i--)
			{
				//Ignore and return nothing.
				if (assetSplit[i].StartsWith('$'))
				{
					return null;
				}
				//Found final directory.
				if (assetSplit[i].Equals("Assets"))
					break;
				//The current Sprite.
				if (assetSplit[i].Equals(assetName))
					continue;
				//Subfolder.
				if (assetSplit[i].StartsWith('.'))
				{
					subFolder.Add(assetSplit[i]);
				}
			}
			subFolder.Reverse();

			if (buffer.Length >= bufferMaxSize)
			{
				Console.WriteLine(ErrorCode.MaximumSizeOverflow(assetName));
				return null;
			}

			string folderStructure = "Assets";
			foreach (string folder in subFolder)
			{
				folderStructure += $"{folder}";
			}

			return new SpriteAssetReference()
			{
				Name = assetName.Split('.')[0],
				Buffer = buffer,
				SubFolder = subFolder.ToArray(),
				FolderStructure = folderStructure,
			};
		}

		private string CreateSpriteProperty(SpriteAssetReference asset, int scriptLine)
		{
			stringBuilder.Clear();

			string assetName = asset.Name
				.Replace("_", " ")
				.Replace("-", " ")
				.Replace("(", "")
				.Replace(")", "")
				.Replace("/", "")
				.Replace("@", "")
				.Replace("\\", "")
				.ToPascalCase()
				.Replace(" ", "");

			stringBuilder.Append($"\tpublic static Sprite {assetName}");
			stringBuilder.Append(" { get; } = \n\t\t Sprite.Asset");
			stringBuilder.Append($"(\"{asset.Name}\", {asset.Library}, {asset.Buffer.Length}, {asset.Offset});");

			Console.WriteLine($"\t[{scriptLine}]asset linked {asset.Library} {asset.FolderStructure}.{asset.Name} [{asset.Buffer.Length}, {asset.Offset}]");
			return stringBuilder.Normalize();
		}

		#endregion

		#region Audio

		public void GenerateAudioLibrary(string libraryPath)
		{

		}

		#endregion

		#region Static

		private string[] LoadAllAssets(string path, params string[] searchPatterns)
		{
			List<string> collection = new List<string>();
			LoadAllAssets(collection, path, searchPatterns);
			return collection.ToArray();
		}

		private static void LoadAllAssets(List<string> collection, string path, params string[] searchPatterns)
		{
			foreach (string sp in searchPatterns)
			{
				string[] files = Directory.GetFiles(path, sp, SearchOption.AllDirectories);
				foreach (string file in files)
				{
					collection.Add(file);
				}
			}
		}

		private static void LocateFolder(string root, string target, ref string? folderPath, params Func<string, bool>[] ignoreConditions)
		{
			if (File.GetAttributes(root) != FileAttributes.Directory)
				return;

			string[] directories = Directory.GetDirectories(root);
			foreach (string directory in directories)
			{
				if (File.GetAttributes(directory) != FileAttributes.Directory)
					continue;

				string[] folderSplit = directory.Split('\\');
				string folder = folderSplit[folderSplit.Length - 1];
				bool ignore = false;
				foreach(Func<string, bool> func in ignoreConditions)
				{
					if(func(folder))
					{
						ignore = true;
						break;
					}
				}
				if (ignore)
					continue;
				if (folderPath != null)
					break;

				if (folder.Equals(target, StringComparison.CurrentCultureIgnoreCase))
				{
					folderPath = directory;
					return;
				}
				else
				{
					if (Directory.GetDirectories(directory).Length == 0)
						continue;
					else
						LocateFolder(directory, target, ref folderPath, ignoreConditions);
				}
			}
		}

		private static void LocateFileDirectory(string root, string target, ref string? path, params Func<string, bool>[] ignoreConditions)
		{

		}
		#endregion
	}
}