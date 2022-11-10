using CosmosEngine;
using CosmosEngine.CoreModule;

namespace Opgave
{
	public class GameWorld : Game
	{
		public override void Initialize()
		{
			BackgroundColour = Colour.DesaturatedBlue;
			//shipSprite = new ContentSprite("log_complete");
			//m_sprite = new Sprite("log_complete");
		}

		public override void Start()
		{
			GameObject go = new GameObject();
			go.Transform.Position = new Vector2(2, 0);
			go.AddComponent<SpriteRenderer>().Sprite = (Sprite)Assets.PlayerShip1Green;

			//go = new GameObject();
			//go.Transform.Position = new Vector2(-2, 0);
			//go.AddComponent<SpriteRenderer>().Sprite = m_sprite;
		}

		private void Iterate(int i)
		{
			Debug.Log($"Value: {i}");
		}

		public override void Update()
		{

		}
	}
}