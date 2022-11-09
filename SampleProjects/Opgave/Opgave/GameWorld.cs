using CosmosEngine;
using CosmosEngine.CoreModule;

namespace Opgave
{
	public class GameWorld : Game
	{
		public override void Initialize()
		{

		}

		public override void Start()
		{
			MinMaxInt i = new MinMaxInt(10, 100);

			i.For((int v) => Debug.Log(v));
		}

		public override void Update()
		{

		}
	}
}