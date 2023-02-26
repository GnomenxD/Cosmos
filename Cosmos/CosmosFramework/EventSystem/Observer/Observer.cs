using CosmosFramework.EventSystems.Base;
using CosmosFramework.Modules;
using System;

namespace CosmosFramework.EventSystems
{
	public class Observer<T> : ObserverBase
	{
		private readonly T observed;
		private readonly Predicate<T> match;
		private readonly Action callback;

		public Observer(T observed, Predicate<T> match, Action callback)
		{
			this.observed = observed;
			this.match = match;
			this.callback = callback;
			ObjectDelegater.OnInstantiate(this);
		}

		internal override void Execute() => callback.Invoke();

		internal override bool Match() => match.Invoke(observed);
	}
}
