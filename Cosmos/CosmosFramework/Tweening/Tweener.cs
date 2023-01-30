using CosmosFramework.Modules;
using System;

namespace CosmosFramework.Tweening
{
	public abstract class TweenerBase
	{
		protected object value;
		private bool done;

		public bool Done { get => done; internal set => done = value; }

		public TweenerBase()
		{
			TweensModule.Tween(this);
		}

		internal abstract void Do();

		public T Value<T>() => (T)value;
	}

	public class TweenerUnmanaged<T> : TweenerBase where T : unmanaged
	{
		internal override void Do()
		{
			throw new NotImplementedException();
		}
	}

	public class Tweener<T> : TweenerBase
	{
		public delegate void MethodNameDelegate(ref T y);
		private Func<T, T> func;

		public event Action onResult;
		public event Action onComplete;

		public event MethodNameDelegate resultMethod;

		public T GValue => Value<T>();

		public Tweener(T value) : base()
		{
			this.value = value;
			resultMethod = new MethodNameDelegate((ref T r) =>
			{
				r = GValue;
			});
		}

		public void Set(Func<T, T> func)
		{
			this.func = func;
		}

		internal override void Do()
		{
			this.value = func.Invoke((T)value);
			OnResultEvent();
		}

		internal void Complete()
		{
			OnCompleteEvent();
			Done = true;
		}

		protected void OnResultEvent() => onResult?.Invoke();
		protected void OnCompleteEvent() => onComplete?.Invoke();

		public Tweener<T> Result(ref T value)
		{
			return this;
		}

		public Tweener<T> OnComplete(Action onComplete)
		{
			this.onComplete += onComplete;
			return this;
		}

		public override string ToString()
		{
			return $"Tweener<{typeof(T)}> {func}";
		}
	}
}