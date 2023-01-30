using System;
using System.Threading.Tasks;


namespace CosmosFramework
{
	public partial class Debug
	{
		public static void Task<T>(Action<T?> action, T? state) where T : class
		{
			Action<object?> method = (Action<object?>)action.Clone();
			Task task = new Task(method, state);
			var i = task.GetAwaiter();
		}
	}
}