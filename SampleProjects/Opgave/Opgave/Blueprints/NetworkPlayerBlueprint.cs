using CosmosFramework;

namespace Opgave.Prefabs
{
	internal class NetworkPlayerBLueprint : Blueprint<NetworkPlayer, NetworkPlayerBLueprint>
	{
		protected override void Create(BlueprintParam param)
		{
			NetworkPlayer player = AddComponent<NetworkPlayer>();
		}
	}
}