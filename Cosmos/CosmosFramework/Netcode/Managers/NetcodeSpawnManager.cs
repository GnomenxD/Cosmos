using System.Collections.Generic;

namespace CosmosFramework.Netcode
{
	public class NetcodeSpawnManager
	{
		private readonly Dictionary<ulong, NetcodeObject> spawnedNetcodeObjects = new Dictionary<ulong, NetcodeObject>();
		private readonly Dictionary<ulong, NetcodeBehaviour> spawnedNetcodeBehaviours = new Dictionary<ulong, NetcodeBehaviour>();

		public Dictionary<ulong, NetcodeObject> SpawnedNetcodeObjects => spawnedNetcodeObjects;
		public Dictionary<ulong, NetcodeBehaviour> SpawnedNetcodeBehaviours => spawnedNetcodeBehaviours;
	}
}