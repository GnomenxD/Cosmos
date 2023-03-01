using CosmosFramework.Modules;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace CosmosFramework.EventSystems
{
	public class Observer<T> : Observer
	{
		private T observedObject;
		private Predicate<T> observedCondition;
		private event Action<T> actionCallback;
		private Task taskCallback;
		private Func<T, IEnumerator> coroutineCallback;
		private Recurrence recurrence;
		private int recursions;
		private float delay;

		public T Object { get => observedObject; set => observedObject = value; }
		internal float Delay => delay;

		private Observer(T observedObject, Predicate<T> observedCondition, Recurrence recurrence, int recursions, float delay) : base(delay)
		{
			this.observedObject = observedObject;
			this.observedCondition = observedCondition;
			this.recurrence = recurrence;
			this.recursions = recursions;
			this.delay = delay;
		}

		public Observer(T observedObject, Predicate<T> observedCondition, Action<T> callback, Recurrence recurrence, int recursions = 1, float delay = 0.0f) : this(observedObject, observedCondition, recurrence, recursions, delay)
		{
			this.actionCallback = callback;
		}

		public Observer(T observedObject, Predicate<T> observedCondition, Task callback, Recurrence recurrence, int recursions = 1, float delay = 0.0f) : this(observedObject, observedCondition, recurrence, recursions, delay)
		{
			this.taskCallback = callback;
		}

		public Observer(T observedObject, Predicate<T> observedCondition, Func<T, IEnumerator> callback, Recurrence recurrence, int recursions = 1, float delay = 0.0f) : this(observedObject, observedCondition, recurrence, recursions, delay)
		{
			this.coroutineCallback = callback;
		}

		public void End() => Alive = false;

		public void AssignEvent(Action<T> callback)
		{
			actionCallback += callback;
		}

		public void AssignEvent(Task<T> callback)
		{
			taskCallback = callback;
		}

		public void AssignEvent(Func<T, IEnumerator> callback)
		{
			coroutineCallback = callback;
		}

		public override bool Condition() => Alive && observedCondition.Invoke(observedObject) && (Delta <= 0.0f);

		public override bool TryInvoke()
		{
			if (Condition())
				Invoke();
			return Alive;
		}

		internal override void Invoke()
		{
			actionCallback?.Invoke(observedObject);
			taskCallback?.Start();
			if (coroutineCallback != null)
			{
				Coroutine.Start(coroutineCallback.Invoke(observedObject));
			}

			switch (recurrence)
			{
				case Recurrence.Once:
					End();
					break;
				case Recurrence.Repeating:
					recursions--;
					if (recursions <= 0)
						End();
					break;
			}
			Delta = delay;
		}

	}
}
