using CosmosFramework;
using CosmosFramework.CoreModule;
using Microsoft.Xna.Framework.Graphics;

namespace Opgave
{
	public class GameWorld : Game
	{
		public override void Initialize()
		{

		}
		public override void Start()
		{
			GameObject gameObject = new GameObject("My Object");
			gameObject.Transform.Position = new Vector2(3, 0);
			SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();

			Texture2D texture = null;
			texture = texture.LoadFromSharedAssets(0, 3043, 28566);

			Sprite sprite = new Sprite(texture);
			sr.Sprite = sprite;

			SpriteRenderer newSr = Object.Instantiate<SpriteRenderer>(gameObject);
			newSr.Sprite = Assets.UfoGreen;
			newSr.Transform.Position = new Vector2(-3, 0);
		}

		public override void Update()
		{
		}
	}
}