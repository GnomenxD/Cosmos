
namespace CosmosFramework.CoreModule
{
	public interface IObject
	{
		public string Name { get; }
		bool Enabled { get; set; }
		bool Destroyed { get; }
		bool DestroyOnLoad { get; }
	}
}