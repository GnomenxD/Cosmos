
using CosmosFramework.Async;
using CosmosFramework.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CosmosFramework
{
	/// <summary>
	/// The behaviour of <see cref="CosmosFramework.GameObject"/>s is controlled by the <see cref="CosmosFramework.Component"/>s that are attached to them. Although Cosmos’ built-in Components can be very versatile, to implement your own gameplay features and create your own Components inherit your script from <see cref="CosmosFramework.GameBehaviour"/>. These allow you to trigger game events, modify Component properties over time and respond to user input in any way you like.
	/// </summary>
	public abstract class GameBehaviour : Component
	{
		private readonly List<Coroutine> activeCoroutines;
		public GameBehaviour()
		{
			activeCoroutines = new List<Coroutine>();
		}

		#region Public Methods

		#region Coroutines
		/// <summary>
		/// In most cases using <see cref="CosmosFramework.GameBehaviour.StartCoroutine(IEnumerator)"/> should suffice. However using <see cref="CosmosFramework.GameBehaviour.StartCoroutine(string)"/> with <paramref name="methodName"/> allows the use of <see cref="CosmosFramework.GameBehaviour.StopCoroutine(string)"/> with a specific method name to stop it.
		/// </summary>
		/// <param name="methodName">Name of the method to invoked. (Case sensitive)</param>
		/// <returns></returns>
		public Coroutine StartCoroutine(string methodName) => StartCoroutine(methodName, new object[] { });

		/// <summary>
		/// In most cases using <see cref="CosmosFramework.GameBehaviour.StartCoroutine(IEnumerator)"/> should suffice. However using <see cref="CosmosFramework.GameBehaviour.StartCoroutine(string, object?[]?)"/> with <paramref name="methodName"/> allows the use of <see cref="CosmosFramework.GameBehaviour.StopCoroutine(string)"/> with a specific method name to stop it.
		/// </summary>
		/// <param name="methodName">Name of the method to invoked. (Case sensitive)</param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		/// <exception cref="System.Reflection.TargetParameterCountException"></exception>
		/// <exception cref="System.ArgumentException"></exception>
		/// <exception cref="System.NullReferenceException"></exception>
		public Coroutine StartCoroutine(string methodName, params object[] parameters)
		{
			BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			MethodInfo method = GetType().GetMethod(methodName, flags);
			if (method != null)
			{
				if (method.ReturnType == typeof(IEnumerator))
				{
					if (method.GetParameters().Length == parameters.Length)
					{
						Coroutine coroutine = StartRoutine(() => { return (IEnumerator)method.Invoke(this, parameters); });
						if (coroutine != null)
						{
							coroutine.Name = methodName;
						}
						return coroutine;
					}
					else
					{
						Debug.LogError($"TargetParameterCountException: Parameter count for {method.ParameterName()} was mismatched by {parameters.ParametersTypeToString()}.");
						return null;
						throw new System.Reflection.TargetParameterCountException($"Parameter count for {method.ParameterName()} was mismatched by {parameters.ParametersTypeToString()}.");
					}
				}
				else
				{
					Debug.LogError($"ArgumentException: Trying to start coroutine {methodName}, return type is {method.ReturnType} but needs to be of type {typeof(IEnumerator)} to start as coroutine.");
					return null;
					throw new System.ArgumentException($"Trying to start coroutine {methodName}, return type is {method.ReturnType} but needs to be of type {typeof(IEnumerator)} to start as coroutine.");
				}
			}
			else
			{
				Debug.LogError($"NullReferenceException: Trying to start coroutine {methodName} but no such method exist on {GetType().FullName}.");
				return null;
				throw new System.NullReferenceException($"Trying to start coroutine {methodName} but no such method exist on {GetType().FullName}.");
			}
		}

		public Coroutine StartRoutine(Func<IEnumerator> routine) => StartRoutine(CoroutineManager.StartCoroutine(routine.Invoke()));
		public Coroutine StartRoutine<T>(Func<T, IEnumerator> routine, T arg) => StartRoutine(CoroutineManager.StartCoroutine(routine.Invoke(arg)));
		public Coroutine StartRoutine<T1, T2>(Func<T1, T2, IEnumerator> routine, T1 arg1, T2 arg2) => StartRoutine(CoroutineManager.StartCoroutine(routine.Invoke(arg1, arg2)));
		public Coroutine StartRoutine<T1, T2, T3>(Func<T1, T2, T3, IEnumerator> routine, T1 arg1, T2 arg2, T3 arg3) => StartRoutine(CoroutineManager.StartCoroutine(routine.Invoke(arg1, arg2, arg3)));
		public Coroutine StartRoutine<T1, T2, T3, T4>(Func<T1, T2, T3, T4, IEnumerator> routine, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => StartRoutine(CoroutineManager.StartCoroutine(routine.Invoke(arg1, arg2, arg3, arg4)));
		public Coroutine StartRoutine<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, IEnumerator> routine, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => StartRoutine(CoroutineManager.StartCoroutine(routine.Invoke(arg1, arg2, arg3, arg4, arg5)));
		public Coroutine StartRoutine(Func<object[], IEnumerator> routine, params object[] args) => StartRoutine(CoroutineManager.StartCoroutine(routine.Invoke(args)));

		private Coroutine StartRoutine(Coroutine coroutine)
		{
			if (coroutine != null)
			{
				activeCoroutines.Add(coroutine);
				coroutine.OnRoutineComplete.Add(OnCoroutineComplete);
			}
			return coroutine;
		}

		[Obsolete("StartCoroutine(IEnumerator routine) has been replaced by StartRoutine(Func<IEnumerator> routine) for easier naming convenstion.", false)]
		/// <summary>
		/// Starts a <see cref="CosmosFramework.Coroutine"/>. <inheritdoc cref="CosmosFramework.Coroutine"/>
		/// </summary>
		/// <param name="routine"></param>
		/// <returns></returns>
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			Coroutine coroutine = CoroutineManager.StartCoroutine(routine);
			if (coroutine != null)
			{
				activeCoroutines.Add(coroutine);
				coroutine.OnRoutineComplete.Add(OnCoroutineComplete);
			}
			return coroutine;
		}

		/// <summary>
		/// Stops the first coroutine named <paramref name="methodName"/> invoked from this <see cref="CosmosFramework.GameBehaviour"/>, only coroutines started using <see cref="CosmosFramework.GameBehaviour.StartCoroutine(string)"/> can be stopped using this method.
		/// </summary>
		/// <param name="methodName">Name of the method to stop. (Case sensitive)</param>
		public void StopCoroutine(string methodName)
		{
			if (string.IsNullOrEmpty(methodName))
				return;
			Coroutine coroutine = activeCoroutines.Find(item => item.Name == methodName);
			if (coroutine != null)
				StopCoroutine(coroutine);
		}

		[System.Obsolete("This method is not working, use StopCoroutine(string) instead. This is the only way to stop a Coroutine atm.", false)]
		public void StopRoutine(Func<IEnumerator> routine)
		{
		}

		/// <summary>
		/// Stops the first coroutine using this <paramref name="routine"/> invoked from this <see cref="CosmosFramework.GameBehaviour"/>.
		/// </summary>
		/// <param name="routine"></param>
		[System.Obsolete("This method is not working, use StopCoroutine(string) instead. This is the only way to stop a Coroutine atm.", false)]
		public void StopCoroutine(IEnumerator routine)
		{

		}

		/// <summary>
		/// Stops the given coroutine.
		/// </summary>
		/// <param name="coroutine"></param>
		public void StopCoroutine(Coroutine coroutine)
		{
			coroutine.Stop();
			activeCoroutines.Remove(coroutine);
		}

		/// <summary>
		/// Stops all coroutines running on this <see cref="CosmosFramework.GameBehaviour"/>.
		/// </summary>
		public void StopAllCoroutines()
		{
			foreach (Coroutine routine in activeCoroutines)
			{
				routine.Stop();
			}
			activeCoroutines.Clear();
		}

		private void OnCoroutineComplete(Coroutine coroutine)
		{
			activeCoroutines.Remove(coroutine);
		}

		protected override void OnDestroy()
		{
			StopAllCoroutines();
		}

		#endregion

		#region Invoke
		/// <summary>
		/// Invokes the method <paramref name="methodName"/> after <paramref name="time"/> seconds.
		/// </summary>
		public void Invoke(string methodName, float time) => Invoke(methodName, System.Array.Empty<object>(), time);


		/// <summary>
		/// Invokes the method <paramref name="methodName"/> after <paramref name="time"/> seconds.
		/// </summary>
		public void Invoke(string methodName, object?[]? parameter, float time)
		{
			if (time > 0)
				StartCoroutine(InvokeMethod(methodName, parameter, time));
			else
				Invoke(methodName, parameter);
		}

		private IEnumerator InvokeMethod(string methodName, object?[]? parameter, float time)
		{
			yield return new WaitForSeconds(time);
			Invoke(methodName, parameter);
		}

		#endregion

		#endregion

		#region Conditional Methods
		/// <summary>
		/// Invoked when another object enters a trigger collider attached to this object.
		/// </summary>
		/// <param name="other">The other Collider involved in this collision.</param>
		protected virtual void OnTriggerEnter(Collider other) //<- Collider2D collider
		{

		}

		/// <summary>
		/// Invoked when another object leaves a trigger collider attached to this object.
		/// </summary>
		/// <param name="other">The other Collider involved in this collision.</param>
		protected virtual void OnTriggerExit(Collider other) //<- Collider2D collider
		{

		}

		/// <summary>
		/// Invoked when an incoming collider makes contact with this object's collider.
		/// </summary>
		/// <param name="collision">The Collision data associated with this collision event.</param>
		protected virtual void OnCollision(Collision collision) //<- Collision2D collision
		{

		}

		#endregion
	}
}