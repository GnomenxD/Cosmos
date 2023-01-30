using System;

namespace CosmosFramework.Netcode
{
	[Serializable]
	public class NetcodeVariable<T> : NetVar
	{
		public delegate void OnValueChangedDelegate(T previousValue, T newValue);

		public event OnValueChangedDelegate onValueChanged = delegate { };

		private protected T internalValue;

		public virtual T Value
		{
			get => internalValue;
			set
			{
				//Setting the value before it has been initialized (assigned to a Netcode Behaviour) should cast a warning.
				//And not set the internal value, since we can't be sure if the value is supposed to be uploaded.
				//To set the value before we're online we can use InitialValue instead.

				//Setting the value as the author will change the internal value, mark it as dirty
				//and send the updated value on the next network package.

				//Setting the value as non-author is not possible, cast a warning/error.

				//onValueChanged should be invoked if we're allowed to change the value.
			}
		}

		public virtual T InitialValue
		{
			get => internalValue;
			set
			{
				//Setting this after we're initialized is not possible (cast a warning).
				//The initial value is what we want to send when the object is Spawned.
				//Does not invoke onValueChanged.
			}
		}


		public NetcodeVariable(T value = default(T), OnValueChangedDelegate onValueChanged = default(OnValueChangedDelegate))
		{
			this.internalValue = value;
		}

		private void Set(T value)
		{

		}

		public override object Read()
		{
			return internalValue;
		}
	}
}