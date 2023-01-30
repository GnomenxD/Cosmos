using System.Reflection;

namespace CosmosFramework.Netcode
{
	public static class NetcodeHelper
	{
		public static bool IsSyncVar(this FieldInfo field) => field.GetCustomAttributes(typeof(SyncVarAttribute), true).Length > 0;

		public static bool IsNetcodeVariable(this FieldInfo field) => field.FieldType.IsAssignableTo(typeof(NetVar));
	}
}