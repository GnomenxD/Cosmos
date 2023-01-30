using System;

namespace CosmosFramework
{
	/// <summary>
	/// The RequireComponent attribute automatically adds required components as dependencies.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class RequireComponent : Attribute
	{
		private Type[] requiredComponents;
		public Type[] RequiredComponents => requiredComponents;

		/// <summary>
		/// When a <see cref="CosmosFramework.Component"/> which uses <see cref="CosmosFramework.RequireComponent"/> to a <see cref="CosmosFramework.GameObject"/>, the required component is automatically added to the GameObject. RequireComponent checks for missing dependencies when using <see cref="CosmosFramework.GameObject.AddComponent{T}"/> and <see cref="CoreModule.Object.Destroy(CoreModule.Object)"/> on a component. If an attached components is a dependency that component can't be destroyed.
		/// </summary>
		/// <param name="requiredComponents"></param>
		public RequireComponent(params Type[] requiredComponents)
		{
			this.requiredComponents = requiredComponents;
		}
	}
}