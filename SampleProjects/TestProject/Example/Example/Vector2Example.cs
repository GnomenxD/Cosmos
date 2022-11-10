using CosmosEngine;

internal class Vector2Example : GameBehaviour
{

	protected override void Update()
	{
		Vector2 mousePosition = Camera.Main.ScreenToWorld(InputManager.MousePosition);
		Transform.Rotate(90f * Time.DeltaTime);

		Vector2 difference = (mousePosition - Transform.Position).Normalized;
		float angle = Vector2.SignedAngle(Transform.Up, difference);
		Debug.QuickLog($"Angle to mouse pointer: {angle}");
	}

	protected override void OnDrawGizmos()
	{
		Gizmos.DrawRay(Transform.Position, Vector2.Up, Colour.Green);
		Gizmos.DrawRay(Transform.Position, Transform.Up, Colour.Blue);
	}
}
