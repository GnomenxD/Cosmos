using System;

namespace CosmosFramework.Netcode
{
	[Serializable]
	public class NetcodeVariable<T>
	{
		public delegate void OnValueChangedDelegate(T previousValue, T newValue);

		public event OnValueChangedDelegate onValueChanged = delegate { };

		private protected T internalValue;

		public virtual T Value
		{
			get => internalValue;
			set
			{

			}
		}

		public NetcodeVariable(T value = default(T), OnValueChangedDelegate onValueChanged = default(OnValueChangedDelegate))
		{

		}

		private void Set(T value)
		{

		}
	}
}