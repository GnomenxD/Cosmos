﻿using CosmosEngine;
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
			//m_sprite = new Sprite("log_complete");
		}

		public override void Start()
		{

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
			if (InputState.Pressed(CosmosEngine.InputModule.Keys.Space))
			{
				GameObject go = new GameObject();
				go.Transform.Position = new Vector2(2, 0);
				go.AddComponent<SpriteRenderer>().Sprite = new Sprite("Assets/Sprites/playerShip1_blue.png");
			}
		}
	}
}