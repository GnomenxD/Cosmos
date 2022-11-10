
namespace CosmosEngine.Modules
{
	public interface IModule
	{
		int ExecutionOrder { get; set; }
		void Initialize();
	}
}
