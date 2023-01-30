using CosmosFramework;

namespace IsometicProject
{
	public class GameWorld : CosmosFramework.CoreModule.Game
	{
		public override void Initialize()
		{

		}

		public override void Start()
		{
			IsometicMap.CreateMap(5, 5);
		}

		public override void Update()
		{
			float hori = InputManager.GetAxis("Horizontal");
			float vert = InputManager.GetAxis("Vertical");
			Vector2 move = new Vector2(hori, vert);
			Camera.Main.Position += (8f * Time.DeltaTime * move);
		}
	}
}