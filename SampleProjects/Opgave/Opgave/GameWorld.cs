using CosmosFramework;
using CosmosFramework.CoreModule;
using System;
using System.IO;
using System.Windows.Forms;

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
			SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
			sr.Sprite = Assets.PlayerShip1Green;

		}

		public override void Update()
		{
		}
	}
}