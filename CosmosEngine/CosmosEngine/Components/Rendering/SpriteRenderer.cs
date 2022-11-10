
using CosmosEngine.CoreModule;
using CosmosEngine.Rendering;

namespace CosmosEngine
{
	public class SpriteRenderer : RenderComponent
	{
		[KeepReference]
		private Sprite sprite;

		public Sprite Sprite 
		{
			get => sprite;
			set
			{
				if(sprite != null)
				{
					sprite.SpriteContentModified -= SpriteModifiedEvent;
				}

				sprite = value;
				//Debug.Log($"1 Sprite Size: {sprite.Size} | {sprite.Texture}");
				//sprite.Load();
				//Debug.Log($"2 Sprite Size: {sprite.Size}");
				if (sprite != null)
				{
					sourceRect = new Rect(Offset, sprite.Size);
					sprite.SpriteContentModified += SpriteModifiedEvent;
				}
			}
		}

		public SpriteRenderer()
		{
			sprite = DefaultGeometry.Square;
		}

		public SpriteRenderer(Sprite sprite)
		{
			this.Sprite = sprite;
		}

		private void SpriteModifiedEvent()
		{
			sourceRect = Sprite.GetSpriteRect();
		}

		public override void Render()
		{
			if (sprite == null)
				return;

			//Core.SpriteBatch.Draw(Sprite.Texture, Transform.Position.ToXna(), Colour);
			//return;

			Debug.Log($"Rect: {sourceRect} +++ {Sprite.Size}");

			Draw.Sprite(Sprite, Transform.Position, Transform.Rotation, Transform.Scale, sourceRect, Vector2.Half, Colour, SortingValue);
		}

		public override string ToString()
		{
			return base.ToString() + $" {(Sprite == null ? "null" : Sprite.Texture.Name)}";
		}
	}
}