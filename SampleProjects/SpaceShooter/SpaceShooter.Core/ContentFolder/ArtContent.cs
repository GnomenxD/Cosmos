using CosmosEngine;

namespace SpaceShooter
{
	public static class ArtContent
	{
		private static Sprite playerShip;
		private static Sprite enemyShip;
		private static Sprite effectYellow;
		private static Sprite effectPurple;

		public static Sprite PlayerShip => playerShip ??= new Sprite("spr_playerShip");
		public static Sprite EnemyShip => enemyShip ??= new Sprite("spr_enemyShip");
		public static Sprite EffectYellow => effectYellow ??= new Sprite("spr_effect_yellow");
		private static Sprite EffectPurple => effectPurple ??= new Sprite("spr_effect_purple");
	}
}