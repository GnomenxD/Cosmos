using System.Reflection;

namespace CosmosFramework.Netcode
{
	public static class NetcodeHelper
	{
		public static bool IsSyncVar(this FieldInfo field)
		{
			object[] fieldMarkers = field.GetCustomAttributes(typeof(SyncVarAttribute), true);
			return fieldMarkers.Length > 0;
		}
	}
}