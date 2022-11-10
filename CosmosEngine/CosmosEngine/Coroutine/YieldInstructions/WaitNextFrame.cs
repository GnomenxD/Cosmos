namespace CosmosEngine.Async
{
	public class WaitNextFrame : YieldInstruction
	{
		private bool previousFrame;
		internal bool framePassed;
		/// <summary>
		/// <inheritdoc cref="CosmosEngine.WaitForSeconds"/>
		/// </summary>
		/// <param name="seconds">Suspends the coroutine execution for the given amount of <paramref name="seconds"/> using scaled time.</param>

		public WaitNextFrame() { }

		public override bool KeepWaiting
		{
			get
			{

				return framePassed;
			}
		}

		//public override void Complete() => ScriptableObject.Instance<Wait.WaitForSecondsPool>().Return(this);

		public override string ToString() => $"{GetType().Name}";
	}
}