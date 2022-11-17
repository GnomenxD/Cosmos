namespace AssetLibraryBuilder
{
	internal class SpriteAssetReference
	{
		private string name;
		private string[] subFolder;
		private string folderStructure;
		private int library;
		private byte[] buffer;
		private int offset;

		public string Name { get => name; set => name = value; }
		public string[] SubFolder { get => subFolder; set => subFolder = value; }
		public string FolderStructure { get => folderStructure; set => folderStructure = value; }
		public int Library { get => library; set => library = value; }
		public byte[] Buffer { get => buffer; set => buffer = value; }
		public int Offset { get => offset; set => offset = value; }
	}
}