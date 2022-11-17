using System;

namespace CosmosFramework
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class KeepReference : Attribute
	{

	}
}