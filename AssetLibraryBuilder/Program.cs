// See https://aka.ms/new-console-template for more information
using AssetLibraryBuilder;

internal class Program
{
	private static void Main(string[] args)
	{
		if (args.Length <= 1)
		{
			Console.WriteLine(ErrorCode.ArgumentOutOfRange(null));
			return;
		}

		string rootDirectory = args[0];
		string configurationName = args[1];

		//for (int i = 0; i < args.Length; i++)
		//{
		//	Console.WriteLine($"\t[{i}] = {args[i]}");
		//}

		LibraryBuilder libraryBuilder = new LibraryBuilder(rootDirectory, configurationName);
		libraryBuilder.CreateLibrary();

		//Console.WriteLine($"Library: {library}");

		//string[] files = System.IO.Directory.GetFiles(rootDirectory, "*.png");
		//for(int i = 0; i < files.Length; i++)
		//	Console.WriteLine($"FILE - {files[i]}");
	}
}