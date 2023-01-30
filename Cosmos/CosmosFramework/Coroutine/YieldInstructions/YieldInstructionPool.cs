using CosmosFramework.Factory;
using CosmosFramework.ObjectPooling;

namespace CosmosFramework.Async
{
	internal class YieldInstructionPool<T> : Pool<T> where T : YieldInstruction, new()
	{
		protected override IFactory<T> Factory => null;

		protected override T Create()
		{
			return new T();
		}
	}
}