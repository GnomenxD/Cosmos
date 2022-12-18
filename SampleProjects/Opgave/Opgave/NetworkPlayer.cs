using CosmosFramework;
using CosmosFramework.Netcode;

namespace Opgave
{
	internal class NetworkPlayer : GameBehaviour
	{
		private NetcodeVariable<int> value = new NetcodeVariable<int>();

		protected override void Start()
		{
			value.onValueChanged += ValueChanged;
		}

		private void ValueChanged(int preValue, int newValue)
		{

		}
	}
}