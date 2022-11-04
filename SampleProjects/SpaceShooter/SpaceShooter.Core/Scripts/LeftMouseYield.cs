using CosmosEngine.Async;

namespace SpaceShooter.Scripts
{
	internal class LeftMouseYield : YieldInstruction
	{
		public override bool KeepWaiting => throw new System.NotImplementedException();
	}
}