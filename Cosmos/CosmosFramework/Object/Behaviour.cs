
namespace CosmosFramework.CoreModule
{
	/// <summary>
	/// Base class for a game feature that has continuously and conditional functionality that is invoked within the game. A <see cref="CosmosFramework.CoreModule.Behaviour"/> is also a reference to a <see cref="CosmosFramework.SceneManagement.Scene"/> object, that will always have a <see cref="CosmosFramework.Transform"/> attached to it, such as <see cref="CosmosFramework.GameObject"/> and <see cref="CosmosFramework.Component"/>.
	/// </summary>
	public abstract class Behaviour : Object, IBehaviour
	{
		private bool started = false;
		public abstract Transform Transform { get; }
		/// <summary>
		/// Returns if the <see cref="CosmosFramework.CoreModule.Behaviour"/> has invoked the <see cref="CosmosFramework.CoreModule.Behaviour.Start"/> method.
		/// </summary>
		internal bool Started => started;

		#region Initial Methods
		internal void InvokeStart()
		{
			if (started)
				return;
			started = true;
			Start();
		}
		/// <summary>
		/// Start is invoked before the first update frame when the script is enabled for the first time, just before any of the Continuous methods are called the first time.
		/// </summary>
		protected virtual void Start()
		{

		}

		#endregion

		#region Continuous Methods
		/// <summary>
		/// Update is invoked every frame, if the Object is enabled.
		/// </summary>
		protected virtual void Update()
		{

		}

		/// <summary>
		/// LateUpdate is invoked every frame, if the Object is enabled. LateUpdate is invoked after all Update methods have been invoked.
		/// </summary>
		protected virtual void LateUpdate()
		{

		}

		/// <summary>
		/// Implementation of OnDrawGizmos allows for drawing debug only shapes and figures that are not visible when running the game in release. Use <see cref="CosmosFramework.Gizmos"/> or <see cref="CosmosFramework.Rendering.Draw"/> to render the desired shapes.
		/// </summary>
		protected virtual void OnDrawGizmos()
		{

		}

		#endregion

		#region Public Methods
		public abstract T AddComponent<T>() where T : Component;
		public abstract Component AddComponent(System.Type componentType);
		public abstract T? GetComponent<T>() where T : class;
		public abstract Component? GetComponent(System.Type componentType);
		public abstract T[] GetComponents<T>() where T : class;
		public abstract Component[] GetComponents(System.Type componentType);
		/// <summary>
		/// Will return <see langword="true"/> if an instance of <typeparamref name="T"/> exist on this <see cref="CosmosFramework.GameObject"/> and set <paramref name="component"/> to the first instance, returns <see langword="false"/> otherwise.
		/// /// <para>An alternative to <see cref="CosmosFramework.CoreModule.Behaviour.GetComponent{T}"/>, can be used to only execute if the requested <see cref="CosmosFramework.Component"/> exists.</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="component"></param>
		/// <returns></returns>
		public bool TryGetComponent<T>(out T? component) where T : Component
		{
			component = GetComponent<T>();
			if (component == null)
				return false;
			else
				return true;
		}

		/// <summary>
		/// Will return <see langword="true"/> if an instance of <paramref name="componentType"/> exist on this <see cref="CosmosFramework.GameObject"/> and set <paramref name="component"/> to the first instance, returns <see langword="false"/> otherwise.
		/// <para>An alternative to <see cref="CosmosFramework.CoreModule.Behaviour.GetComponent(System.Type)"/>, can be used to only execute if the requested <see cref="CosmosFramework.Component"/> exists.</para>
		/// </summary>
		/// <param name="componentType"></param>
		/// <param name="component"></param>
		/// <returns></returns>
		public bool TryGetComponent(System.Type componentType, out Component? component)
		{
			component = GetComponent(componentType);
			if (component == null)
				return false;
			else
				return true;
		}

		#endregion
	}
}