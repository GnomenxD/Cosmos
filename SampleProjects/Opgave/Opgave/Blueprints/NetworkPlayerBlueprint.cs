using CosmosFramework;

namespace Opgave.Blueprint
{
	internal class NetworkPlayerBlueprint : Blueprint<NetworkPlayer, NetworkPlayerBlueprint>
	{
		protected override string UniqueName => "Network Player";

		protected override void Create()
		{
			NetworkPlayer player = AddComponent<NetworkPlayer>();
		}
	}
}