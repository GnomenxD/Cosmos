namespace CosmosEngine.CoreModule
{
	internal struct SharedAssetReference
	{
		private readonly int library;
		private readonly int bufferSize;
		private readonly int offset;

		public int Library => library;
		public int BufferSize => bufferSize;
		public int Offset => offset;

		public SharedAssetReference(int library, int bufferSize, int offset)
		{
			this.library = library;
			this.bufferSize = bufferSize;
			this.offset = offset;
		}
	}
}