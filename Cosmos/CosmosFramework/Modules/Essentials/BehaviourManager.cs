
using Cosmos.Collections;
using CosmosFramework.CoreModule;
using System.Collections.Generic;
using System.Reflection;

namespace CosmosFramework.Modules
{
	/// <summary>
	/// <see cref="CosmosFramework.Modules.BehaviourManager"/> is responsible for for invoking methods: Start, Update and LateUpdate on <see cref="CosmosFramework.CoreModule.Behaviour"/> classes. When a <see cref="CosmosFramework.CoreModule.Behaviour"/> is instantiated it will be seperated into collections that handle invoking the methods, if a <see cref="CosmosFramework.CoreModule.Behaviour"/> is instantiated without using any of the methods, it will be ignored in the game loop, wasting as little resources as necessary.
	/// </summary>
	public sealed class BehaviourManager : ObserverManager<Behaviour, BehaviourManager>
	{
		private const string UpdateMethod = "Update";
		private const string LateUpdateMethod = "LateUpdate";
		private readonly List<Behaviour> startBehaviours = new List<Behaviour>();
		private readonly DirtyList<Behaviour> updateBehaviours = new DirtyList<Behaviour>();
		private readonly DirtyList<Behaviour> lateUpdateBehaviours = new DirtyList<Behaviour>();
		private readonly List<GameObject> prefabObjects = new List<GameObject>();

		public override void Initialize()
		{
			base.Initialize();
			ObjectDelegater.CreateNewDelegation<Behaviour>(Subscribe);
		}

		protected override void Add(Behaviour item)
		{
			System.Type t = item.GetType();
			bool isUpdateBehaviour = false;
			bool isLateUpdateBehaviour = false;
			do
			{
				if (!isUpdateBehaviour)
				{
					MethodInfo updateMethod = item.GetType().GetMethod(UpdateMethod, DefaultFlags);
					if (updateMethod != null && updateMethod.DeclaringType == t)
					{
						isUpdateBehaviour = true;
					}
				}

				if (!isLateUpdateBehaviour)
				{
					MethodInfo lateMethod = item.GetType().GetMethod(LateUpdateMethod, DefaultFlags);
					if (lateMethod != null && lateMethod.DeclaringType == t)
					{
						isLateUpdateBehaviour = true;
					}
				}

				t = t.BaseType;
			} while (t != typeof(Behaviour));
			startBehaviours.Add(item);
			if (isUpdateBehaviour)
				updateBehaviours.Add(item);
			if (isLateUpdateBehaviour)
				lateUpdateBehaviours.Add(item);
		}

		public override void BeginEventCall()
		{
			foreach (Behaviour behaviour in updateBehaviours)
			{
				if (behaviour.Destroyed)
				{
					updateBehaviours.IsDirty = true;
					continue;
				}

				if (behaviour.Enabled && behaviour.Started)
				{
					behaviour.Invoke(UpdateMethod);
				}
			}

			foreach (Behaviour behaviour in lateUpdateBehaviours)
			{
				if (behaviour.Destroyed)
				{
					lateUpdateBehaviours.IsDirty = true;
					continue;
				}
				if (behaviour.Enabled && behaviour.Started)
				{
					behaviour.Invoke(LateUpdateMethod);
				}
			}

			foreach (Behaviour behaviour in startBehaviours)
			{
				if (behaviour.Destroyed || !behaviour.Enabled)
				{
					continue;
				}
				behaviour.InvokeStart();
			}
			startBehaviours.Clear();
		}

		public override void Update()
		{
			base.Update();
			if (updateBehaviours.IsDirty)
				updateBehaviours.DisposeAll(RemoveAllPredicate());
			if (lateUpdateBehaviours.IsDirty)
				lateUpdateBehaviours.DisposeAll(RemoveAllPredicate());
		}

		public override System.Predicate<Behaviour> RemoveAllPredicate() => item => item.Destroyed;

		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
			{
				prefabObjects.Clear();
				updateBehaviours.Clear();
				lateUpdateBehaviours.Clear();
				startBehaviours.Clear();
			}
			base.Dispose(disposing);
		}
	}
}