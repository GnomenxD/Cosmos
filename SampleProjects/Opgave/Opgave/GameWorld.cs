using CosmosEngine;
using CosmosEngine.CoreModule;
using System;
using System.Collections;
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
			string path = $"{AppDomain.CurrentDomain.BaseDirectory}Assets/Sprites/playerShip1_green.png";
			Debug.Log($"Path: {path}");

			byte[] bytes = File.ReadAllBytes(path);



			StringBuilder sb = new StringBuilder();
			using (StreamReader reader = new StreamReader(path))
			{
				int index = 0;
				string line;
				while((line = reader.ReadLine()) != null)
				{
					sb.AppendLine($"[{index}]: {line}");
					index++;
				}
			}
			Debug.Log(sb.ToString());

			string s = string.Empty;
			foreach(byte b in bytes)
			{
				s += b.ToString();
			}
			Debug.Log($"{bytes.Length}: {s}");
		}

		private void Iterate(int i)
		{
			Debug.Log($"Value: {i}");
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