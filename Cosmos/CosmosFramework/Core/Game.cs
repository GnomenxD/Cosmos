
using CosmosFramework.Modules;
using System;
using System.Collections.Generic;

namespace CosmosFramework.CoreModule
{
	public abstract class Game : GameModule<Game>, IStartModule, IUpdateModule
	{
		private static Game game;
		private static readonly List<IModule> gameModules = new List<IModule>();
		private Colour backgroundColour = Colour.EditorBlue;

		private int screenResolutionWidth;
		private int screenResolutionHeight;
		private bool fullscreen;

		internal List<IModule> GameModules => gameModules;
		public Colour BackgroundColour { get => backgroundColour; set => backgroundColour = value; }
		internal int ResolutionWidth => screenResolutionWidth;
		internal int ResolutionHeight => screenResolutionHeight;
		internal bool Fullscreen => fullscreen;

		/// <summary>
		/// Initialize is invoked only once, before the first frame and before start. Instantiation should not occur within Initialize, use Start instead.
		/// </summary>
		public override abstract void Initialize();
		[System.Obsolete("The Load Content method is obsolete as content should be implemented without using the Content Manager. The method is still required for the Game to work but will be removed in a further version.", false)]
		/// <summary>
		/// Use AddContentLoader(IContentLoader) to load content files in this method.
		/// </summary>
		public virtual void LoadContent()
		{

		}
		/// <summary>
		/// Start is invoked only once, before the first frame update.
		/// </summary>
		public abstract void Start();
		/// <summary>
		/// Update is called once per frame.
		/// </summary>
		public abstract void Update();

		/// <summary>
		/// Creates a new <typeparamref name="T"/> from where instantiation of <see cref="CosmosFramework.GameObject"/> can occur and the initial Game Logic. Only a single instance of <see cref="CosmosFramework.CoreModule.Game"/> will be executed at any time. Creating a <see langword="new"/> <see cref="CosmosFramework.CoreModule.Game"/> will add the essential <see cref="CosmosFramework.Modules.GameModule"/> components. Further functionality can be accessed by adding additional <see cref="CosmosFramework.Modules.GameModule"/> components, use <see cref="CosmosFramework.CoreModule.Game.AddModule{T}"/>, some modules will only exist while the game is in EDITOR mode. After <see cref="CosmosFramework.CoreModule.Game.LaunchApplication{T}"/> has been invoked no further <see cref="CosmosFramework.Modules.GameModule"/> components can be added or removed.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Game Create<T>() where T : Game, new()
		{
			gameModules.Clear();
			game = new T();
			game.ExecutionOrder = -2;

			game.Resolution(ScreenResolution.m_720p, false);
			game.AddDefault();
			game.AddEssential();
#if EDITOR
			game.AddEditor();
#elif DEBUG
			game.AddDebug();
#endif
			return game;
		}

		private Game AddEssential()
		{
			game.AddModule<ObjectManager>(-1);
			game.AddModule<BehaviourManager>(0);
			game.AddModule<RenderManager>(-1000);

			return this;
		}

		/// <summary>
		/// Adds default <see cref="CosmosFramework.Modules.GameModule"/> components to the <see cref="CosmosFramework.CoreModule.Game"/>.
		/// </summary>
		/// <returns></returns>
		public Game AddDefault()
		{
			AddModule<CoroutineManager>(1);
			AddModule<InputManager>(-800);
			AddModule<InputSystem>(-800);
			AddModule<EventManager>();
			AddModule<AudioSystem>(1);
			AddModule<TweensModule>(2);

			return this;
		}

		public Game AddDebug()
		{
			game.AddModule<Debug>(-900);
			return this;
		}

		public Game AddEditor()
		{
			AddDebug();
			game.AddModule<GizmosModule>(-900);
			game.AddModule<EditorGrid>(-900);
			game.AddModule<EditorStats>(-900);

			return this;
		}

		/// <summary>
		/// Adds the <see cref="CosmosFramework.Modules.GameModule"/> <typeparamref name="T"/> to the <see cref="CosmosFramework.CoreModule.Game"/>, multiple modules of the same type cannot be added. After <see cref="CosmosFramework.CoreModule.Game.LaunchApplication{T}"/> has been invoked no further <see cref="CosmosFramework.Modules.GameModule"/> components can be added or removed.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public Game AddModule<T>(int executionOrder = 0) where T : IModule => AddModule(Activator.CreateInstance<T>(), executionOrder);

		/// <summary>
		/// Adds an instance of a <see cref="CosmosFramework.Modules.GameModule"/> to the game, will check for duplicates.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="module"></param>
		/// <returns></returns>
		internal Game AddModule<T>(T module, int executionOrder = 0) where T : IModule
		{
			if (Core.ApplicationIsRunning)
			{
				Debug.Log($"Not possible to add game modules once the game has been launched, make sure to add all required modules before running Game.LaunchApplication().", LogFormat.Error);
				return this;
			}
			if (gameModules.Exists(item => item.GetType() == typeof(T)))
			{
				Debug.Log($"Not possible to add multiple game modules of the same type ({typeof(T).Name}).", LogFormat.Warning);
			}
			else
			{
				module.ExecutionOrder = executionOrder;
				gameModules.Add(module);
			}
			return this;
		}

		/// <summary>
		/// Removes the <see cref="CosmosFramework.Modules.GameModule"/> <typeparamref name="T"/> from the <see cref="CosmosFramework.CoreModule.Game"/>, essential modules cannot be removed. After <see cref="CosmosFramework.CoreModule.Game.LaunchApplication{T}"/> has been invoked no further <see cref="CosmosFramework.Modules.GameModule"/> components can be added or removed.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public Game RemoveModule<T>() where T : IModule
		{
			if(Core.ApplicationIsRunning)
			{
				//Debug.Log($"Not possible to remove game modules once the game has been launched.", LogFormat.Error);
				return this;
			}
			if (gameModules.Exists(item => item.GetType() == typeof(T)))
			{
				gameModules.RemoveAll(item => item.GetType() == typeof(T));
			}
			else
			{
				//Debug.Log($"Not possible to remove game module {typeof(T).Name} since it does not exist.", LogFormat.Warning);
			}
			return this;
		}

		public Game Resolution(int width, int height, bool fullscreen)
		{
			this.screenResolutionWidth = width;
			this.screenResolutionHeight = height;
			this.fullscreen = fullscreen;
			return this;
		}

		public Game Resolution(ScreenResolution resolution, bool fullscreen)
		{
			this.screenResolutionWidth = resolution.Width();
			this.screenResolutionHeight = resolution.Height();
			this.fullscreen = fullscreen;
			return this;
		}

		/// <summary>
		/// Launches the <see cref="CosmosFramework.CoreModule.Game"/>.
		/// </summary>
		public static void StartApplication()
		{
			using (game)
			{
				Core.StartApplication(game);
			}
		}

		/// <summary>
		/// Use <see cref="CosmosFramework.CoreModule.Game.LaunchApplication"/> as the final augment to start the application.
		/// </summary>
		public void LaunchApplication()
		{
			gameModules.Add(game);
			StartApplication();
		}

		public static void ExitApplication()
		{
			Core.Instance.CloseApplication();
		}

		public void CloseApplication()
		{
			ExitApplication();
		}
	}
}