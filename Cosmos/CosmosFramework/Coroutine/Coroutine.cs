
using CosmosFramework.EventSystems;
using System.Collections;
using System.Collections.Generic;
using CosmosFramework.Async;
using CosmosFramework.Modules;

namespace CosmosFramework
{
	/// <summary>
	/// A coroutine is a function that can suspend its execution (yield) until the given <see cref="CosmosFramework.Async.YieldInstruction"/> finishes. Yielding of any type, including null, results in the execution coming back on a later frame, unless the coroutine is stopped or has complete.
	/// </summary>
	public sealed class Coroutine
	{
		#region Fields
		private Stack<IEnumerator> routines = new Stack<IEnumerator>();
		private readonly Event<Coroutine> onRoutineCompleteEvent = new Event<Coroutine>();
		private string name;
		private CoroutineState coroutineState;

		/// <summary>
		/// Represents whether the <see cref="CosmosFramework.Coroutine"/> is running (not complete) or not.
		/// </summary>
		internal bool IsAlive => !(State == CoroutineState.Stopped || State == CoroutineState.Interrupted);
		/// <summary>
		/// The name is reference when starting the <see cref="CosmosFramework.Coroutine"/> using a string, this way it can be stopped through the use a string as well.
		/// </summary>
		internal string Name { get => name; set => name = value; }
		/// <summary>
		/// An <see cref="EventSystems.Event"/> that is invoked once the <see cref="CosmosFramework.Coroutine"/> has completed running successfully. If the coroutine is stopped through a StopCoroutine this method will not be invoked, since it was interrupted doing its execution.
		/// </summary>
		public Event<Coroutine> OnRoutineComplete => onRoutineCompleteEvent;
		/// <summary>
		/// The current <see cref="CoroutineState"/> of this <see cref="Coroutine"/>.
		/// </summary>
		public CoroutineState State { get => coroutineState; private set => coroutineState = value; }
		#endregion

		#region Public Methods
		internal void Initialise(IEnumerator routine)
		{
			onRoutineCompleteEvent.RemoveAllListeners();
			routines.Clear();
			routines.Push(routine);
			State = CoroutineState.Running;
		}
		/// <summary>
		/// Pauses the <see cref="CosmosFramework.Coroutine"/> all functionality is stopped until <see cref="CosmosFramework.Coroutine.Resume"/> is invoked. <see cref="CosmosFramework.Async.YieldInstruction"/> will also wait until the coroutine is running.
		/// </summary>
		public void Pause() => State = CoroutineState.Paused;
		/// <summary>
		/// Resumes the <see cref="CosmosFramework.Coroutine"/> if it has been paused using <see cref="CosmosFramework.Coroutine.Pause"/>.
		/// </summary>
		public void Resume()
		{
			if (State == CoroutineState.Paused)
				State = CoroutineState.Running;
		}
		internal void Stop() => State = CoroutineState.Interrupted;
		internal void Cancel() => State = CoroutineState.Cancelled;

		internal void Update()
		{
			if (routines.Count > 0)
			{
				if (routines.Peek().Current is YieldInstruction yieldInstruction)
				{
					if (yieldInstruction.KeepWaiting)
					{
						//CoreModule.EngineCore.Debug.Add("Waiting: " + yieldInstruction.ToString());
						State = CoroutineState.Waiting;
						return;
					}
					else
					{
						yieldInstruction.Complete();
					}
				}
			}
			//CoreModule.EngineCore.Debug.Add("Running");

			if (routines.Peek().MoveNext() == false)
			{
				routines.Pop();

				if (routines.Count == 0)
				{
					State = CoroutineState.Stopped;
					OnRoutineComplete.Invoke(this);
					return; //We are done.
				}
			}
			else if(routines.Peek().Current is IEnumerator)
			{
				routines.Push((IEnumerator)routines.Peek().Current);
			}
			else
			{
				State = CoroutineState.Running;
			}
		}

		#endregion

		#region Static Methods

		public static Coroutine Start(IEnumerator routine)
		{
			return CoroutineManager.StartCoroutine(routine);
		}

		public static Coroutine Start(IEnumerator routine, out Cancellation cancellationtoken)
		{
			Coroutine coroutine = CoroutineManager.StartCoroutine(routine);
			cancellationtoken = new Cancellation(() => coroutine.State = CoroutineState.Cancelled);
			return coroutine;
		}

		#endregion
	}
}