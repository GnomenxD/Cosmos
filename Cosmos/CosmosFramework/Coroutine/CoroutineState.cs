
namespace CosmosFramework.Async
{
	public enum CoroutineState
	{
		/// <summary>The <see cref="CosmosFramework.Coroutine"/> has successfully executed its function and has now stopped running.</summary>
		Stopped,
		/// <summary>The <see cref="CosmosFramework.Coroutine"/> is currently executing its function.</summary>
		Running,
		/// <summary>The <see cref="CosmosFramework.Coroutine"/> is waiting for a <see cref="CosmosFramework.Async.YieldInstruction"/> to complete before executing again.</summary>
		Waiting,
		/// <summary>The <see cref="CosmosFramework.Coroutine"/> has been paused using <see cref="CosmosFramework.Coroutine.Pause"/> and will start executing its function again once <see cref="CosmosFramework.Coroutine.Resume"/> is invoked.</summary>
		Paused,
		/// <summary>The <see cref="CosmosFramework.Coroutine"/> was cancelled with the <see cref="CosmosFramework.Async.Cancellation"/> and was not executed to completion.</summary>
		Cancelled,
		/// <summary>The <see cref="CosmosFramework.Coroutine"/> was interrupted using StopCoroutine method and was not executed successfully.</summary>
		Interrupted,
	}
}