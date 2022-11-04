using CosmosEngine;
using CosmosEngine.CoreModule;
using SpaceWar.Cat;

namespace SpaceWar
{
	public class GameWorld : Game
	{
		public override void Initialize()
		{
			BackgroundColour = Colour.DesaturatedBlue;
		}

		public override void LoadContent()
		{
		}

		public override void Start()
		{
			GameObject gameObject = new GameObject("Grid");
			Grid grid = gameObject.AddComponent<Grid>();
		}

		public override void Update()
		{
			float vertical = InputManager.GetAxis("vertical");
			float horizontal = InputManager.GetAxis("horizontal");

			Camera.Main.Transform.Translate(new Vector2(horizontal, vertical) * 4 * Time.DeltaTime);
			Debug.FastLog(Camera.Main.Transform.Position);
		}
	}
}