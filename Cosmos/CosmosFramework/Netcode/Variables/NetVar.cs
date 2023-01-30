using System;

namespace CosmosFramework.Netcode
{
	[Serializable]
	public abstract class NetVar
	{
		private bool isDirty;
		public bool IsDirty { get => isDirty; set => isDirty = value; }
		public abstract object Read();
	}
}