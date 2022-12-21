namespace CosmosFramework
{
	public struct PrefabParams
	{
		private readonly object[] param;
		private int index;

		public PrefabParams(params object[] param)
		{
			this.param = param;
			index = 0;
		}

		public T ReadValue<T>()
		{
			if(param == null)
				return default(T);
			if (index >= param.Length)
				return default(T);
			if (param[index].GetType() != typeof(T))
				return default(T);

			return (T)param[index++];
		}
	}
}