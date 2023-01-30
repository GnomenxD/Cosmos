using CosmosFramework.Async;

namespace CosmosFramework.Tweening
{
	public static class TweenExtension
	{
		public static Tweener<float> Set(this float value, float target, float duration, ref float vOut, out Cancellation cancellationToken)
		{
			cancellationToken = new Cancellation();
			Tweener<float> tweener = new Tweener<float>(value);
			tweener.Set((float v) =>
			{
				float f = Mathf.MoveTowards(v, target, (target - value) * (1.0f / duration) * Time.UnscaledDeltaTime);
				v = f;
				if (v == target)
					tweener.Complete();
				return v;
			});
			return tweener;
		}

		public static Tweener<Transform> MoveX(this Transform transform, float target, float duration) => transform.Move(new Vector2(target, transform.Position.Y), duration);
		public static Tweener<Transform> MoveLocalX(this Transform transform, float target, float duration) => transform.Move(new Vector2(transform.Position.X + target, transform.Position.Y), duration);
		public static Tweener<Transform> MoveY(this Transform transform, float target, float duration) => transform.Move(new Vector2(transform.Position.Y, target), duration);
		public static Tweener<Transform> MoveLocalY(this Transform transform, float target, float duration) => transform.Move(new Vector2(transform.Position.X, transform.Position.Y + target), duration);

		public static Tweener<Transform> Move(this Transform transform, Vector2 target, float duration)
		{
			Tweener<Transform> tweener = new Tweener<Transform>(transform);
			float delta = Vector2.Distance(transform.Position, target) / duration;
			tweener.Set((Transform t) =>
			{
				transform.Position = Vector2.MoveTowards(transform.Position, target, delta * Time.UnscaledDeltaTime);
				if (transform.Position == target)
					tweener.Complete();
				return t;
			});
			return tweener;
		}
	}

	public class TweenData<T>
	{
		private T value;

		public T Value { get => value; set => this.value = value; }

		public TweenData(T value) => this.value = value;
	}
}