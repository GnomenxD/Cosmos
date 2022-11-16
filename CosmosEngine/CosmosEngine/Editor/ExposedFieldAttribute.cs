using System;

namespace CosmosEngine
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class ExposedFieldAttribute : Attribute
	{
		private string displayName;

		public ExposedFieldAttribute()
		{

		}

		public ExposedFieldAttribute(string displayName)
		{
			this.displayName = displayName;
		}
	}
}