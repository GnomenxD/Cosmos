using CosmosEngine;
using CosmosEngine.CoreModule;
using CosmosEngine.Variables;

namespace Opgave
{
	public class GameWorld : Game
	{
		private ContentSprite shipSprite;
		private Sprite m_sprite;

		public override void Initialize()
		{
			BackgroundColour = Colour.DesaturatedBlue;
			shipSprite = new ContentSprite("log_complete");
			m_sprite = new Sprite("log_complete");
		}

		public override void Start()
		{
			GameObject go = new GameObject();
			go.Transform.Position = new Vector2(2, 0);
			go.AddComponent<SpriteRenderer>().Sprite = (Sprite)shipSprite;
			MinMaxInt i = new MinMaxInt(10, 100);

			go = new GameObject();
			go.Transform.Position = new Vector2(-2, 0);
			go.AddComponent<SpriteRenderer>().Sprite = m_sprite;
			i.For((int v) => Debug.Log(v));
		}

		public override void Update()
		{

		}
	}
}