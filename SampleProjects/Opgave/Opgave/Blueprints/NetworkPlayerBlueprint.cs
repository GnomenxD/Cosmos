using CosmosFramework;

namespace Opgave.Blueprints
{
	internal class NetworkPlayerBlueprint : Blueprint<NetworkPlayer, NetworkPlayerBlueprint>
	{
		protected override void Create()
		{
			NetworkPlayer player = AddComponent<NetworkPlayer>();
		}
	}
}