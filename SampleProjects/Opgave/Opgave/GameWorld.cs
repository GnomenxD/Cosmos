using CosmosEngine;
using CosmosEngine.CoreModule;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Opgave
{
	public class GameWorld : Game
	{
		private GameObject go;
		private Sprite m_sprite;

		public override void Initialize()
		{
			m_sprite = new Sprite("Assets/Sprites/playerShip1_green.png");

		}

		public override void Start()
		{
			GameObject go = new GameObject("Test Object", typeof(SpriteRenderer));
			go.GetComponent<SpriteRenderer>().Sprite = Assets.Planet01;
		}

		public override void Update()
		{
			if (InputState.Pressed(CosmosEngine.InputModule.Keys.H))
			{
				m_sprite.Load("Assets/Sprites/playerShip2_red.png");
			}


			if (InputState.Pressed(CosmosEngine.InputModule.Keys.J))
			{
				if (m_sprite == null)
				{
				}
				Debug.Log("Instantiate");
				go = new GameObject();
				go.Transform.Position = new Vector2(2, 0);
				go.AddComponent<SpriteRenderer>().Sprite = m_sprite;
			}

			if(go != null)
			{
				//Debug.Log($"{go.Enabled} + {go.GetComponent<SpriteRenderer>().Enabled}");
				//Debug.Log(go.GetComponent<SpriteRenderer>());
				go.Transform.Translate(new Vector2(InputManager.GetAxis("Horizontal"), InputManager.GetAxis("Vertical")) * 3f * Time.DeltaTime, Space.World);
			}
		}
	}
}