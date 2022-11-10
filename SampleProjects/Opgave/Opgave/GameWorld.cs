using CosmosEngine;
using CosmosEngine.Collections;
using CosmosEngine.CoreModule;
using System.Collections;

namespace Opgave
{
	public class GameWorld : Game
	{
		private RandomAssortment<int> randomAssortment;
		private GameObject go;
		private Sprite m_sprite;

		public override void Initialize()
		{
			m_sprite = new Sprite("Edlow_Blue");
		}

		public override void Start()
		{
			randomAssortment = new RandomAssortment<int>();
			randomAssortment.Fill(() =>
			{
				int value = Random.Range(0, 100);
				return value;
			}, 5000);
		}

		public override void Update()
		{
			Debug.Log($"Random List [{randomAssortment.Span}] - ({randomAssortment.Count})");
			if (InputManager.GetKeyDown(CosmosEngine.InputModule.Keys.Enter))
			{
				int[] ints = randomAssortment.GetRange(300);
				Debug.Log($"Returned: [{ints.Length}]");
			}
			if(InputManager.GetKeyDown(CosmosEngine.InputModule.Keys.C))
			{
				randomAssortment.Reset();
			}
		}
	}
}