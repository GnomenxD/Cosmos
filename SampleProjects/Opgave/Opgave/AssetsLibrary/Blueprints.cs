using Opgave.Blueprint;

namespace Opgave
{
	public static class Blueprints
	{
		internal static PlayerShip PlayerShip => PlayerShipBlueprint.Instantiate();
	}
}