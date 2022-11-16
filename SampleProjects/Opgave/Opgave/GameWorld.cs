using CosmosEngine;
using CosmosEngine.CoreModule;
using System.Collections;
using System.IO;

namespace Opgave
{
	public class GameWorld : Game
	{
		public override void Initialize()
		{

		}

		public override void Start()
		{
			GameObject obj = new GameObject();
			obj.AddComponent<MathExample>();

			Sprite sprite = new Sprite("Assets/Sprites/ufoYellow.png");

			SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
			sr.Sprite = sprite;

		}

		public override void Update()
		{

		}
	}
}