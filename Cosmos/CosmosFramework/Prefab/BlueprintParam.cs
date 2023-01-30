namespace CosmosFramework
{
	public struct BlueprintParam
	{
		private readonly object[] param;
		private int index;

		public BlueprintParam(params object[] param)
		{
			this.param = param;
			index = 0;
		}

		/// <summary>
		/// Reads the current value in the blueprint parameters and moves the indexer to the next point.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="defaultValue">The value to be returned if the type is mismatched or cannot be retrieved.</param>
		/// <returns></returns>
		public T ReadValue<T>(T defaultValue = default)
		{
			if(param == null)
				return defaultValue;
			if (index >= param.Length)
				return defaultValue;
			if (param[index].GetType() != typeof(T))
				return defaultValue;
			return (T)param[index++];
		}
	}
}