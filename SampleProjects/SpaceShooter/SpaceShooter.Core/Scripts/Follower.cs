using CosmosEngine;
using CosmosEngine.InputModule;

namespace SpaceShooter
{
	public class Follower : GameBehaviour
	{
		private Player p;
		public Player Player => p ??= FindObjectOfType<Player>();

		protected override void Start()
		{
			Input.AddInputAction(402, "Split", started: Split, new InputControl(Keys.H));
			Input.AddInputAction(403, "Split", started: SetPlayer, new InputControl(Keys.J));
			Input.AddInputAction(404, "Split", started: SetAsteroid, new InputControl(Keys.K));
		}

		private void SetPlayer(CallbackContext context)
		{
			Transform.SetParent(FindObjectOfType<Player>().Transform, false);
		}

		private void SetAsteroid(CallbackContext context)
		{
			Transform.SetParent(FindObjectOfType<SpriteRenderer>().Transform, true);
		}

		private void Split(CallbackContext context)
		{
			Transform.SetParent(null);
		}
	}
}