namespace CosmosFramework.EventSystems.Base
{
	public abstract class ObserverBase
	{
		internal abstract bool Match();
		internal abstract void Execute();
	}
}
