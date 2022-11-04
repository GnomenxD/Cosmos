using CosmosEngine;
using CosmosEngine.InputModule;
using CosmosEngine.Modules;
using Vector2Xna = Microsoft.Xna.Framework.Vector2;

namespace SpaceShooter
{
	public class Player : GameBehaviour
	{
		private float lastShotTimer;
		private float speed = 6.2f;
		private Vector2 movementInput;
		private float velocity;

		protected override void Awake()
		{
			Input.AddInputAction(100, "Move", started: Move, null, canceled: Move, new InputControl(Keys.W, Interaction.All, Vector2.Up), new InputControl(Keys.S, Interaction.All, Vector2.Down));
			//Input.AddInputAction(101, "Shoot", null, performed: Shoot, null, new InputControl(MouseButton.Left));
		}

		private void Move(CallbackContext context)
		{
			Vector2 movement = context.ReadValue<Vector2>();
			movementInput = movement;
			Debug.Log("Invoked");
		}

		private ProjectilePool projectilePool = new ProjectilePool();

		private void Shoot()
		{
			Projectile projectile = projectilePool.Request();
			projectile.Transform.Position = Transform.Position;
			projectile.Transform.Rotation = Transform.Rotation + Random.Range(-5f, 5f);
			projectile.Activate(projectilePool, 7.8f + velocity);
			projectile.GameObject.Enabled = true;
			lastShotTimer = Time.ElapsedTime + 0.15f;
		}

		protected override void Update()
		{
			Rotate();

			if (InputManager.GetButton("mouse0")) 
			{
				if (lastShotTimer < Time.ElapsedTime) 
				{
					Shoot();
				}
			}

			Transform.Translate(speed * Time.DeltaTime * movementInput, Space.Self);
			velocity = speed * -movementInput.Y;
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
		}

		protected override void OnDrawGizmos()
		{
			Gizmos.DrawWireCircle(Transform.Position, 3f);
		}

		private void Rotate()
		{
			Vector2 mousePosition = Camera.Main.ScreenToWorld(Input.MousePosition);
			Transform.RotateTowards(mousePosition, 270f);
		}

		protected override void OnTriggerEnter(Collider other)
		{
			Debug.Log("OnTriggerEnter " + other.Name);
		}

		protected override void OnDestroy()
		{
			Input.RemoveInputAction(100);
		}
	}
}