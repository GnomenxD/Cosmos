﻿
using System.Runtime.InteropServices;

namespace CosmosFramework.Async
{
	/// <summary>
	/// Base class for all yield instructions.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public abstract class YieldInstruction
	{
		public abstract bool KeepWaiting { get; }
		public virtual void Complete() { }
	}
}