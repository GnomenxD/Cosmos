
using CosmosFramework.Factory;

namespace CosmosFramework.Async
{
	internal class CoroutineFactory : Factory<Coroutine>
	{
		public override Coroutine Create()
		{
			return new Coroutine();
		}
	}
}