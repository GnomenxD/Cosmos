
namespace CosmosFramework.Modules
{
	public interface IModule
	{
		int ExecutionOrder { get; set; }
		void Initialize();
	}
}
