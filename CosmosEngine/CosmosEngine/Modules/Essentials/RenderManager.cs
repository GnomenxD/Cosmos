
using CosmosEngine.CoreModule;
using CosmosEngine.Collections;
using CosmosEngine.Rendering;
using System.Threading;
using System.Collections.Generic;

namespace CosmosEngine.Modules
{
	/// <summary>
	/// <see cref="CosmosEngine.Modules.RenderManager"/> handles all <see cref="IRenderWorld"/> and <see cref="IRenderUI"/> and render calls. <see cref="CosmosEngine.Modules.RenderManager"/> runs on a different <see cref="System.Threading.Thread"/> to handle culling and passing the <see cref="CosmosEngine.CoreModule.IRenderer"/> to the Render/Draw method.
	/// </summary>
	public sealed class RenderManager : ObserverManager<IRenderer, RenderManager>, IRenderModule, IStartModule
	{
		private readonly DirtyList<IRenderWorld> renderComponents = new DirtyList<IRenderWorld>();
		private readonly DirtyList<IRenderUI> uiComponents = new DirtyList<IRenderUI>();

		private readonly List<IRenderWorld> visibleRenderObjets = new List<IRenderWorld>();
		private static readonly object m_lock = new object();
		private bool waitingForRenderQueue;

		private Thread renderThread;

		public override void Initialize()
		{
			base.Initialize();
			ObjectDelegater.CreateNewDelegation<IRenderer>(Subscribe);
		}

		public void Start()
		{
			renderThread = new Thread(new ThreadStart(CullRenderObjects));
			renderThread.IsBackground = true;
			renderThread.Start();
		}

		private void CullRenderObjects()
		{
			while (Application.IsRunning)
			{
				do
				{
					Thread.Sleep(System.TimeSpan.Zero);
				} while (waitingForRenderQueue);

				lock(m_lock)
				{
					visibleRenderObjets.Clear();
					Camera mainCamera = Camera.Main;
					foreach (IRenderWorld render in renderComponents)
					{
						if (render.Transform == null)
							continue;
						if (!render.Enabled)
							continue;
						if (mainCamera.InsideViewFrustrum(render.Transform.Position * 100))
						{
							visibleRenderObjets.Add(render);
						}
					}
					waitingForRenderQueue = true;
				}
			}
		}

		protected override void Add(IRenderer item)
		{
			if(item is IRenderWorld)
			{
				renderComponents.Add((IRenderWorld)item);
			}
			if(item is IRenderUI)
			{
				uiComponents.Add((IRenderUI)item);
			}
		}

		public override void BeginEventCall() {	}

		public void RenderWorld()
		{
			Draw.Space = WorldSpace.World;
			lock (m_lock)
			{
				foreach (IRenderWorld obj in visibleRenderObjets)
				{
					if (obj.Expired)
					{
						renderComponents.IsDirty = true;
						continue;
					}

					if (obj.Enabled)
					{
						obj.Render();
					}
				}
				waitingForRenderQueue = false;
			}

			//int culledObjects = 0;
			//foreach (IRenderWorld obj in renderComponents)
			//{
			//	if (obj.Transform == null)
			//		continue;
			//	if (obj.Expired)
			//	{
			//		renderComponents.IsDirty = true;
			//		continue;
			//	}


			//	if (obj.Enabled)
			//	{
			//		if (!Camera.Main.InsideViewFrustrum(obj.Transform.Position * 100))
			//		{
			//			culledObjects++;
			//			continue;
			//		}
			//		obj.Render();
			//	}
			//}
			//Debug.QuickLog($"Objects culled: {culledObjects}");
		}

		public void RenderUI()
		{
			Draw.Space = WorldSpace.Screen;
			foreach (IRenderUI obj in uiComponents)
			{
				if (obj.Expired)
				{
					uiComponents.IsDirty = true;
					continue;
				}

				if (obj.Enabled)
				{
					obj.UI();
				}
			}
		}
		public override void Update()
		{
			base.Update();
			if (renderComponents.IsDirty)
			{
				renderComponents.DisposeAll(RemoveAllPredicate());
			}
			if (uiComponents.IsDirty)
			{
				uiComponents.DisposeAll(RemoveAllPredicate());
			}
		}
		public override System.Predicate<IRenderer> RemoveAllPredicate() => item => item.Expired;
	}
}