using CosmosFramework.Rendering;

namespace CosmosFramework.Modules
{
	public abstract class RenderModule<TModule> : GameModule<TModule>, IRenderModule where TModule : RenderModule<TModule>
	{
		protected WorldSpace worldSpace;

		public void RenderUI()
		{
			if (worldSpace == WorldSpace.Screen)
				Render();
		}

		public void RenderWorld()
		{
			if (worldSpace == WorldSpace.World)
				Render();
		}

		protected virtual void Render()
		{
			
		}
	}
}