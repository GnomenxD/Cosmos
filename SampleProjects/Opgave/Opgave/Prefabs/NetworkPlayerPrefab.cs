using CosmosFramework;

namespace Opgave.Prefabs
{
	internal class NetworkPlayerPrefab : PrefabObject<NetworkPlayer, NetworkPlayerPrefab>
	{
		protected override void Create(PrefabParams param)
		{
			NetworkPlayer player = AddComponent<NetworkPlayer>();
		}
	}
}