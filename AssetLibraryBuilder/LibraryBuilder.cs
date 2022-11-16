using System.IO;
using System.Text;

namespace AssetLibraryBuilder
{
	internal class LibraryBuilder
	{
		private string? assetFolder;
		private string? binFolder;
		private string rootDirectory;
		private string outputDirectory;
		private StringBuilder stringBuilder;

		public LibraryBuilder(string rootDirectory, string outputDirectory)
		{
			Console.WriteLine($"Generating Asset Library.");
			this.rootDirectory = rootDirectory;
			this.outputDirectory = outputDirectory;
			LocateOutputDirectory();
			stringBuilder = new StringBuilder();

			Console.WriteLine($"root directory: {rootDirectory}");
			Console.WriteLine($"output directory: {outputDirectory}");
		}

		public void CreateLibrary()
		{
			LocateAssetDirectory();
			if (assetFolder == null)
			{
				Console.WriteLine($"No Assets folder was detected... Automatic asset builder could not create a library.");
				return;
			}
			string libraryPath = CreateAssetLibraryFolder();
			GenerateSpriteLibrary(libraryPath);
			GenerateAudioLibrary(libraryPath);

			Console.WriteLine($"Asset Library was successful constructed.");
		}

		#region Folder Structure

		private void LocateAssetDirectory()
		{
			//LocateAssetFolder(rootDirectory, ref assetFolder);
			LocateFolder(rootDirectory, "Assets", ref assetFolder, new Func<string, bool>[]
			{
				(s) => s.StartsWith("."),
				(s) => s.Equals("bin"),
				(s) => s.Equals("obj"),
			});
		}

		private void LocateOutputDirectory()
		{
			LocateFolder(rootDirectory, "bin", ref binFolder, new Func<string, bool>[]
			{
				(s) => s.StartsWith("."),
			});

			string directory = binFolder.Replace($"\\bin", "");
			outputDirectory = $"{directory}\\{outputDirectory}";
		}

		private string CreateAssetLibraryFolder()
		{
			string libraryPath = Directory.GetParent(assetFolder).FullName + $"\\AssetsLibrary";
			Directory.CreateDirectory(libraryPath);
			return libraryPath;
		}

		#endregion

		#region Library Generation

		public void GenerateSpriteLibrary(string libraryPath)
		{
			List<string> spriteAssets = new List<string>();
			LoadAllAssets(spriteAssets, $"{assetFolder}\\Sprites", "*.png", "*.jpg");

			StringBuilder sb = new StringBuilder();
			using (StreamWriter writer = new StreamWriter($"{libraryPath}\\Assets.cs"))
			{
				sb.AppendLine("using CosmosEngine;\n");
				sb.AppendLine("public static partial class Assets");
				sb.AppendLine("{");

				sb.AppendLine("\t#region Sprites");

				foreach (string assetPath in spriteAssets)
				{
					sb.AppendLine(CreateSpriteAssetClass(assetFolder, assetPath));
				}

				sb.AppendLine("\t#endregion");

				sb.AppendLine("}");

				writer.Write(sb.Normalize());
			}
			Console.WriteLine($"libraryPath: {libraryPath}");

			GenerateSharedAssets(spriteAssets);
		}

		private void GenerateSharedAssets(List<string> spriteAssets)
		{
			FileStream stream = File.Open($"{rootDirectory}\\sharedassets0.asset", FileMode.Create);
			StreamWriter writer = new StreamWriter(stream);

			foreach (string assetPath in spriteAssets)
			{
				byte[] bytes = File.ReadAllBytes(assetPath);
				writer.WriteLine(bytes);
			}

			stream.Close();

			Console.WriteLine($"generated sharedasset file");
		}

		public void GenerateAudioLibrary(string libraryPath)
		{

		}

		#endregion

		#region Static
		private string CreateSpriteAssetClass(string assetFolderPath, string path)
		{
			stringBuilder.Clear();

			string[] folderSplit = assetFolderPath.Split('\\');
			string[] assetSplit = path.Split('\\');

			string assetPathName = path.Replace(assetFolderPath, "").Replace("\\", "/").TrimStart('/');
			string assetName = assetSplit[assetSplit.Length - 1]
				.Replace("_", " ")
				.ToPascalCase()
				.Replace(" ", "")
				.Split('.')[0];

			string finalPath = "";
			bool ignore = true;
			for (int i = 0; i < folderSplit.Length; i++)
			{
				if (folderSplit[i].Equals("Assets"))
					ignore = false;
				if (!ignore)
					finalPath = folderSplit[i] + "/";
			}

			stringBuilder.Append($"\tpublic static Sprite {assetName}");
			stringBuilder.Append(" { get; } = \n\t\tnew Sprite");
			stringBuilder.Append($"(\"{finalPath + assetPathName}\");");

			Console.WriteLine($"\tasset link: {assetPathName}");

			return stringBuilder.Normalize();
		}

		private void LoadAllAssets(List<string> collection, string path, params string[] searchPatterns)
		{
			foreach (string sp in searchPatterns)
			{
				string[] files = Directory.GetFiles(path, sp, SearchOption.AllDirectories);
				foreach (string file in files)
				{
					collection.Add(file);
				}
			}

			//string[] directories = Directory.GetDirectories(path);
			//foreach (string directory in directories)
			//{
			//	if (File.GetAttributes(directory) != FileAttributes.Directory)
			//		continue;
			//	string[] folderSplit = directory.Split('\\');
			//	string folder = folderSplit[folderSplit.Length - 1];
			//	if (folder.StartsWith(".") || folder.Equals("bin") || folder.Equals("obj"))
			//		continue;
			//	if (Directory.GetDirectories(directory).Length == 0)
			//		continue;
			//	else
			//		LoadAllAssets(collection, directory, searchPatterns);
			//}
		}

		private void LocateFolder(string root, string target, ref string? folderPath, params Func<string, bool>[] ignoreConditions)
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
					Console.WriteLine($"target {target} found: {directory}");
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
		#endregion
	}
}