using CosmosEngine;
using CosmosEngine.CoreModule;
using CosmosEngine.Rendering;

namespace SpaceShooter
{
	public class GameWorld : Game
	{
		public override void Initialize()
		{
			BackgroundColour = new Colour(13, 37, 69, 255);
		}

		public override void LoadContent()
		{
		}

		public override void Start()
		{
			GameObject go = new GameObject("CameraRig");
			go.AddComponent<CameraRig>();

			GameObject prefab = new GameObject("Asteroid");
			prefab.AddComponent<SpriteRenderer>().Sprite = DefaultGeometry.Circle;
			prefab.AddComponent<BoxCollider>();
			prefab.AddComponent<Rigidbody>();
			for (int i = 0; i < 3; i++) {
				Vector2 position = Random.InsideUnitCircle() * Random.Range(2f, 4f);
				GameObject gameObject = Object.Instantiate<GameObject>(prefab, position);
				gameObject.Name = "Asteroids [" + i + "]";
			}
			Object.Destroy(prefab);

			GameObject obj = new GameObject("Player");
			obj.Transform.Position = new Vector2(1.5f, 1.0f);
			obj.AddComponent<SpriteRenderer>().Sprite = ArtContent.PlayerShip;
			obj.AddComponent<Player>();
			obj.AddComponent<BoxCollider>().Offset = new Vector2(0, -0.3f);
			obj.AddComponent<Rigidbody>();
			obj.AddComponent<BoxCollider>().Size = new Vector2(1.5f, 1.5f);

			follower = new GameObject("Follower");
			follower.Transform.SetParent(obj.Transform);
			follower.AddComponent<Follower>();
			follower.AddComponent<BoxCollider>().Size = new Vector2(0.5f, 0.5f);
			for (int i = 0; i < 10; i++)
			{
				GameObject enemy = new GameObject("Enemy 1", typeof(Enemy));
			}
		}

		private GameObject follower;

		public override void Update()
		{
			//Debug.FastLog(follower.Transform);
			//Debug.FastLog("Follower Parent: " + follower.Transform.Parent);
		}
	}
}