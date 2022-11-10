using CosmosEngine;
using CosmosEngine.CoreModule;

namespace Opgave
{
	public class GameWorld : Game
	{
		private ContentSprite shipSprite;
		private Sprite m_sprite;

		public override void Initialize()
		{
			BackgroundColour = Colour.DesaturatedBlue;
			//shipSprite = new ContentSprite("log_complete");
			m_sprite = new Sprite("Edlow_Blue");
			m_sprite.LoadThroughContentManager();
		}

		public override void Start()
		{
			//GameObject go = new GameObject();
			//go.Transform.Position = new Vector2(2, 0);
			//go.AddComponent<SpriteRenderer>().Sprite = (Sprite)Assets.PlayerShip1Green;

			GameObject go = new GameObject();
			go.Transform.Position = new Vector2(-2, 0);
			go.AddComponent<SpriteRenderer>().Sprite = m_sprite;

		}

		private void Iterate(int i)
		{
			Debug.Log($"Value: {i}");
		}

		public override void Update()
		{
			if(InputManager.GetKeyDown(CosmosEngine.InputModule.Keys.Space))
			{
				GameObject go = new GameObject();
				go.Transform.Position = new Vector2(2, 0);
				go.AddComponent<SpriteRenderer>().Sprite = Assets.PlayerShip1Green;
			}
		}
	}
}