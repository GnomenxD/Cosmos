using CosmosFramework.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosFramework.EventSystems
{
	public abstract class Observer
	{
		private bool alive;
		private float delta;
		public bool Alive { get => alive; protected set => alive = value; }
		internal float Delta { get => delta; set => delta = value; }

		internal Observer(float intialDelay)
		{
			this.alive = true;
			this.delta = intialDelay;
			ObjectDelegater.OnInstantiate(this);
		}
	
		public abstract bool Condition();
		public abstract bool TryInvoke();
		internal abstract void Invoke();

		public static Observer<T> Once<T>(T obj, Predicate<T> condition, Action callback)
			=> new Observer<T>(obj, condition, (_) => { callback.Invoke(); }, Recurrence.Once);
		public static Observer<T> Once<T>(T obj, Predicate<T> condition, Action<T> callback)
			=> new Observer<T>(obj, condition, callback, Recurrence.Once);
		public static Observer<T> Once<T>(T obj, Predicate<T> condition, Task callback)
			=> new Observer<T>(obj, condition, callback, Recurrence.Once);
		public static Observer<T> Once<T>(T obj, Predicate<T> condition, Func<Task> callback)
			=> new Observer<T>(obj, condition, callback.Invoke(), Recurrence.Once);
		public static Observer<T> Once<T>(T obj, Predicate<T> condition, Func<T, Task> callback)
			=> new Observer<T>(obj, condition, callback.Invoke(obj), Recurrence.Once);
		public static Observer<T> Once<T>(T obj, Predicate<T> condition, Func<T, IEnumerator> callback)
			=> new Observer<T>(obj, condition, callback, Recurrence.Once);

		public static Observer<T> Recurring<T>(T obj, Predicate<T> condition, Action callback, int recursions, float delay)
			=> new Observer<T>(obj, condition, (_) => { callback.Invoke(); }, Recurrence.Repeating, recursions, delay);
		public static Observer<T> Recurring<T>(T obj, Predicate<T> condition, Action<T> callback, int recursions, float delay) 
			=> new Observer<T>(obj, condition, callback, Recurrence.Repeating, recursions, delay);
		public static Observer<T> Recurring<T>(T obj, Predicate<T> condition, Func<T, IEnumerator> callback, int recursions, float delay)
			=> new Observer<T>(obj, condition, callback, Recurrence.Repeating, recursions, delay);

		public static Observer<T> Continuous<T>(T obj, Predicate<T> condition, Action callback, float delay)
			=> new Observer<T>(obj, condition, (_) => { callback.Invoke(); }, Recurrence.Continuous, delay: delay);
		public static Observer<T> Continuous<T>(T obj, Predicate<T> condition, Action<T> callback, float delay) 
			=> new Observer<T>(obj, condition, callback, Recurrence.Continuous, delay: delay);
		public static Observer<T> Continuous<T>(T obj, Predicate<T> condition, Func<T, IEnumerator> callback, float delay)
			=> new Observer<T>(obj, condition, callback, Recurrence.Continuous, delay: delay);
	}
}
