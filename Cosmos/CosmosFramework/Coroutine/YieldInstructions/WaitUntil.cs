using System;

namespace CosmosFramework.Async
{
	/// <summary>
	/// Used to suspend a <see cref="CosmosFramework.Coroutine"/> execution <see langword="until"/> the given condition is <see langword="true"/>.
	/// </summary>
	public class WaitUntil : YieldInstruction
	{

		internal Func<bool> predicate;
		public override bool KeepWaiting => !predicate();

		public WaitUntil() => predicate = () => { return false; };
		public WaitUntil(Func<bool> predicate) => this.predicate = predicate;

		public override void Complete() => ScriptableObject.Instance<Wait.WaitUntilPool>().Return(this);
	}
}