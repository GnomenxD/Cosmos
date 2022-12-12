
using CosmosFramework.Collections;
using CosmosFramework.CoreModule;
using System.Collections.Generic;

namespace CosmosFramework.Modules
{
	/// <summary>
	/// <see cref="CosmosFramework.Modules.ObjectManager"/> holds references to all <see cref="CosmosFramework.CoreModule.Object"/> which have been instantiated. This is useful for looking for any references that currently exist at runtime. It is responsible for invoking the Awake method.
	/// </summary>
	public sealed class ObjectManager : ObserverManager<Object, ObjectManager>
	{
		private readonly DirtyList<GameObject> gameObjects = new DirtyList<GameObject>();

		public override void Initialize()
		{
			base.Initialize();
			ObjectDelegater.CreateNewDelegation<Object>(Subscribe);
		}

		protected override void Add(Object item)
		{
			base.Add(item);
			if (!item.IsAwake)
			{
				item.InvokeAwake();
				if (item.Enabled)
					item.Invoke("OnEnable");
			}
			if (item is GameObject go)
				gameObjects.Add(go);
		}

		public override void BeginEventCall() 
		{
			foreach(Object item in observerList)
			{
				if(item.Destroyed)
				{
					observerList.IsDirty = true;
					continue;
				}
				if(item.MarkedForDestruction)
				{
					if(item.TimeToDestroy <= 0)
						item.DestroyImmediate();
					else
						item.TimeToDestroy -= Time.DeltaTime;
				}
			}
		}

		public override void Update()
		{
			base.Update();
			if(gameObjects.IsDirty)
			{
				gameObjects.RemoveAll(RemoveAllPredicate());
				gameObjects.IsDirty = false;
			}
		}

		public override System.Predicate<Object> RemoveAllPredicate() => item => item.Destroyed;

		#region FindObjectOfType
		/// <summary>
		/// Returns the first loaded, active and enabled object of a type.
		/// </summary>
		/// <returns></returns>
		internal static T FindObjectOfType<T>() where T : Object
		{
			return FindObjectOfType<T>(false);
		}

		/// <summary>
		/// Returns the first loaded object of a type, can include inactive (not enabled).
		/// </summary>
		/// <returns></returns>
		internal static T FindObjectOfType<T>(bool includeInactive) where T : Object
		{
			return Instance.observerList.Find(item => (item.GetType() == typeof(T) || item.GetType().IsSubclassOf(typeof(T))) && (item.Enabled || includeInactive) && !item.Destroyed) as T;
		}

		/// <summary>
		/// Returns a list of all loaded, active and enabled objects of a type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		internal static T[] FindObjectsOfType<T>() where T : Object
		{
			return FindObjectsOfType<T>(false);
		}

		/// <summary>
		/// Returns a list of all loaded objects of a type, can include inactive (not enabled).
		/// </summary>
		/// <returns></returns>
		internal static T[] FindObjectsOfType<T>(bool includeInactive) where T : Object
		{
			List<T> list = new List<T>();
			List<Object> searched = Instance.observerList.FindAll(item =>
				(item.GetType() == typeof(T) ||
				item.GetType().IsSubclassOf(typeof(T))) &&
				(item.Enabled || includeInactive) &&
				!item.Destroyed);

			foreach (Object obj in searched)
			{
				list.Add(obj as T);
			}
			return list.ToArray();
		}

		/// <summary>
		/// Returns a list of all loaded, active and enbaled objects.
		/// </summary>
		/// <returns></returns>
		internal static Object[] FindObjectsOfAll()
		{
			return FindObjectsOfAll(false);
		}

		/// <summary>
		/// Returns a list of all loaded objects, can include inactive (not enabled).
		/// </summary>
		/// <param name="includeInactive"></param>
		/// <returns></returns>
		internal static Object[] FindObjectsOfAll(bool includeInactive)
		{
			return Instance.observerList.FindAll(item => (item.Enabled || includeInactive) && !item.Destroyed).ToArray();
		}
		#endregion

		#region GameObject.Find
		/// <summary>
		/// <inheritdoc cref="CosmosFramework.Modules.ObjectManager.Find(string, bool)"/>
		/// </summary>
		/// <returns>This function only returns active GameObjects. If no GameObject with name can be found, null is returned.</returns>
		public static GameObject Find(string name) => Find(name, false);
		/// <summary>
		/// Finds a <see cref="CosmosFramework.GameObject"/> by <paramref name="name"/> and returns it.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="includeInactive"></param>
		/// <returns>If no GameObject with name can be found, null is returned.</returns>
		public static GameObject Find(string name, bool includeInactive)
		{
			foreach(GameObject go in Instance.gameObjects)
			{
				if(go.Destroyed)
				{
					Instance.gameObjects.IsDirty = true;
					continue;
				}

				if (go.Enabled || includeInactive)
				{
					if (go.Name.Equals(name))
					{
						return go;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the first active <see cref="CosmosFramework.GameObject"/> tagged with <paramref name="tag"/>.
		/// </summary>
		public static GameObject FindGameObjectWithTag(string tag) => FindGameObjectWithTag(tag, false);
		/// <summary>
		/// Returns the first <see cref="CosmosFramework.GameObject"/> tagged with <paramref name="tag"/>. 
		/// </summary>
		public static GameObject FindGameObjectWithTag(string tag, bool includeInactive)
		{
			foreach (GameObject go in Instance.gameObjects)
			{
				if (go.Destroyed)
				{
					Instance.gameObjects.IsDirty = true;
					continue;
				}

				if (go.Enabled || includeInactive)
				{
					if (go.CompareTag(tag))
					{
						return go;
					}
				}
			}
			return null;
		}
		/// <summary>
		/// Returns an array of active <see cref="CosmosFramework.GameObject"/>s tagged with <paramref name="tag"/>.
		/// </summary>
		public static GameObject[] FindGameObjectsWithTag(string tag) => FindGameObjectsWithTag(tag, false);
		/// <summary>
		/// Returns an array of <see cref="CosmosFramework.GameObject"/>s tagged with <paramref name="tag"/>.
		/// </summary>
		public static GameObject[] FindGameObjectsWithTag(string tag, bool includeInactive)
		{
			List<GameObject> gameObjects = new List<GameObject>();
			foreach (GameObject go in Instance.gameObjects)
			{
				if (go.Destroyed)
				{
					Instance.gameObjects.IsDirty = true;
					continue;
				}

				if (go.Enabled || includeInactive)
				{
					if (go.CompareTag(tag))
					{
						gameObjects.Add(go);
					}
				}
			}
			return gameObjects.ToArray();
		}

		#endregion
	}
}
