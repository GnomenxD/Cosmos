namespace CosmosFramework
{
	public enum LoadingMode
	{
		/// <summary>
		/// Will load resource immidielty when instantiated.
		/// <para>PRO: Eager loading will load the resource as soon as the program is executed, this will ensure that initialization of resources are always ready and can be referenced from one frame to another.</para>
		/// <para>CON: Could introduce longer loading times, when resources are not in use, they will occupy memory.</para>
		/// </summary>
		EagerLoading,
		/// <summary>
		/// Will load resource once a request is made.
		/// <para>PRO: Lazy loading allows for resources to be loaded only when nessecary, they can also be discarded once they're not in use anymore. Since they would just get reloaded once a reference is requested again.</para>
		/// <para>CON: Could delay the initialization of resources and could introduce performance drops at runtime.</para>
		/// </summary>
		LazyLoading
	}
}