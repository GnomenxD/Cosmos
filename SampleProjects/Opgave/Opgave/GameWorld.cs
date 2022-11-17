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
			Debug.Log("Exists: " + File.Exists("sharedassets0.asset"));
			StreamReader sReader = new StreamReader("sharedassets0.asset");

			Sprite[] sprites = new Sprite[2];
			int i = 0;

			Debug.Log("shared assets total count: " + File.ReadAllBytes("sharedassets0.asset").Length);

			using (var tempFile = new TempFileCollection())
			{
				int offset = 2698;
				byte[] buffer = new byte[2708];
				//sReader.BaseStream.Read(buffer, 0, 286506);
				sReader.BaseStream.Position = 286506 + 2698;
				//sReader.BaseStream.Read(buffer, 0, 2698);
				sReader.BaseStream.Read(buffer, 0, buffer.Length);

				string file = tempFile.AddExtension("png");
				File.WriteAllBytes(file, buffer);
				Debug.Log($"File: {file}");
				sprites[i] = new Sprite();
				sprites[i].Load(file);
				Console.WriteLine(file);
			}
			sReader.Close();

			GameObject go = new GameObject("Test Object", typeof(SpriteRenderer));
			go.GetComponent<SpriteRenderer>().Sprite = sprites[0];
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