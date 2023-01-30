using CosmosFramework;
using CosmosFramework.CoreModule;

namespace Opgave
{
	public class GameWorld : Game
	{
		public override void Initialize()
		{
		}

		public override void Start()
		{
			int[] values = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			Debug.LogTable(values[8..]);

			Item item = new Item();	
			var product = new { item.name, item.price };	

			GameObject go = new GameObject();
		}

		public class Item
		{
			public string name;
			public int price;
		}

		public override void Update()
		{
		}
	}


}