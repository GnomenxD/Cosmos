﻿
using System;
using System.Collections;
using CosmosFramework.Async;

namespace CosmosFramework.Modules
{
	public sealed class CoroutineManager : ObserverManager<Coroutine, CoroutineManager>
	{
		public static Coroutine StartCoroutine(IEnumerator routine)
		{
			if(!ActiveAndEnabled)
			{
				return null;
			}
			Coroutine coroutine = CoroutinePool.Get();
			coroutine.Initialise(routine);
			Singleton.SubscribeItem(coroutine);
			return coroutine;
		}
		public static void StartCoroutine(Coroutine coroutine) => Singleton.SubscribeItem(coroutine);
		public static void StopCoroutine(Coroutine coroutine) => coroutine.Stop();

		public override void BeginEventCall()
		{
			foreach (Coroutine coroutine in observerList)
			{
				if(!coroutine.IsAlive)
				{
					CoroutinePool.Release(coroutine);
					observerList.IsDirty = true;
					continue;
				}
				if (coroutine.State == CoroutineState.Paused)
					continue;

				coroutine.Update();
				if (coroutine.State == CoroutineState.Cancelled)
					coroutine.Stop();
			}
		}
		public override Predicate<Coroutine> RemoveAllPredicate() => item => !item.IsAlive;

		protected override void Dispose(bool disposing)
		{
			if(!IsDisposed && disposing)
			{
				observerList.Clear();
			}
			base.Dispose(disposing);
		}
	}
}