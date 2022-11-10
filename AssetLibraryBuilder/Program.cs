// See https://aka.ms/new-console-template for more information
using AssetLibraryBuilder;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
	private static string? assetFolder;
	private static StringBuilder stringBuilder;

	private static void Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("No solution directory was provided... Please check Pre-build event in the .csproj file.");
			return;
		}

		string rootDirectory = args[0];
		SearchForAssetFolder(rootDirectory, ref assetFolder);

		if(assetFolder == null)
		{
			Console.WriteLine($"No Assets folder was detected... Automatic asset builder could not create a library.");
			return;
		}

		stringBuilder = new StringBuilder();

		List<string> spriteAssets = new List<string>();
		LoadAllAssets(spriteAssets, $"{assetFolder}\\Sprites", "*.png", "*.jpg");

		string libraryPath = Directory.GetParent(assetFolder).FullName + $"\\AssetsLibrary";
		Directory.CreateDirectory(libraryPath);

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
		//Console.WriteLine($"Library: {library}");

		//string[] files = System.IO.Directory.GetFiles(rootDirectory, "*.png");
		//for(int i = 0; i < files.Length; i++)
		//	Console.WriteLine($"FILE - {files[i]}");
	}

	private static string CreateSpriteAssetClass(string assetFolderPath, string path)
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
		for(int i = 0; i < folderSplit.Length; i++)
		{
			if (folderSplit[i].Equals("Assets"))
				ignore = false;
			if(!ignore)
				finalPath = folderSplit[i] + "/";
		}

		stringBuilder.Append($"\tpublic static Sprite {assetName}");
		stringBuilder.Append(" { get; } = new Sprite");
		stringBuilder.Append($"(\"{finalPath + assetPathName}\");");

		Console.WriteLine($"Building asset link: {assetPathName}");

		return stringBuilder.Normalize();
	}

	private static void LoadAllAssets(List<string> collection, string path, params string[] searchPatterns)
	{
		foreach (string sp in searchPatterns)
		{
			string[] files = Directory.GetFiles(path, sp, SearchOption.AllDirectories);
			foreach(string file in files)
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

	private static void SearchForAssetFolder(string path, ref string asset)
	{
		if (File.GetAttributes(path) != FileAttributes.Directory)
			return;
		string[] directories = Directory.GetDirectories(path);
		foreach (string directory in directories)
		{
			if (File.GetAttributes(directory) != FileAttributes.Directory)
				continue;
			string[] folderSplit = directory.Split('\\');
			string folder = folderSplit[folderSplit.Length - 1];
			if (folder.StartsWith(".") || folder.Equals("bin") || folder.Equals("obj"))
				continue;
			if (assetFolder != null)
				break;

			if (folder.Equals("Assets", StringComparison.CurrentCultureIgnoreCase))
			{
				Console.WriteLine($"ASSET FOLDER: {directory}");
				asset = directory;
				return;
			}
			else
			{
				if (Directory.GetDirectories(directory).Length == 0)
					continue;
				else
					SearchForAssetFolder(directory, ref asset);
			}
		}
	}
}