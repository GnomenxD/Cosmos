using CosmosEngine;

internal class Vector2Example : GameBehaviour
{
	private Vector2 direction;

	protected override void Update()
	{
		Vector2 mousePosition = Camera.Main.ScreenToWorld(InputManager.MousePosition);
		Transform.Rotate(InputManager.GetAxis("horizontal") * 90f * Time.DeltaTime);

		direction = (mousePosition - Transform.Position).Normalized;
		float angle = Vector2.SignedAngle(Transform.Up, direction);
		Debug.QuickLog($"Angle to mouse pointer: {angle:F0}");
	}

	protected override void OnDrawGizmos()
	{
		Gizmos.DrawRay(Transform.Position, direction, Colour.Blue);
		Gizmos.DrawRay(Transform.Position, Transform.Up, Colour.Red);
		Gizmos.DrawRay(Transform.Position, Vector2.Up, Colour.Green);
	}
}
