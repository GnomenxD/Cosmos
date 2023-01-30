using System;

namespace CosmosFramework.Async
{
	public struct Cancellation
	{
		private Action cancellationCallback;

		internal Cancellation(Action cancellationCallback)
		{
			this.cancellationCallback = cancellationCallback;
		}
		
		public void Cancel()
		{
			cancellationCallback?.Invoke();
		}
	}
}