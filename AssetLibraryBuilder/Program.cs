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
		if (args.Length <= 1)
		{
			Console.WriteLine("No solution and output directory was provided... Please check Pre-build event in the .csproj file.");
			return;
		}

		string rootDirectory = args[0];
		string outputDirectory = args[1];

		LibraryBuilder libraryBuilder = new LibraryBuilder(rootDirectory, outputDirectory);
		libraryBuilder.CreateLibrary();

		//Console.WriteLine($"Library: {library}");

		//string[] files = System.IO.Directory.GetFiles(rootDirectory, "*.png");
		//for(int i = 0; i < files.Length; i++)
		//	Console.WriteLine($"FILE - {files[i]}");
	}
}