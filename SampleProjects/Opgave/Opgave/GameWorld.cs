using CosmosFramework;
using CosmosFramework.CoreModule;
using CosmosFramework.InputModule;
using CosmosFramework.Tweening;
using Opgave.Blueprints;
using System;
using System.Reflection;

namespace Opgave
{
	public class GameWorld : Game
	{
		private Flag data;
		private PlayerShip ship;
		private float value;

		public override void Initialize()
		{
			data = new Flag();
		}
		public override void Start()
		{

			EnemyShipBlueprint.Instantiate(new Vector2(5.0f, 3.0f), 90.0f, 100);
			ship = PlayerShipBlueprint.Instantiate(Vector2.Zero, 0.0f, 100, 5, 10);
			EnemyShipBlueprint.Instantiate(new Vector2(-5.0f, 3.0f), 270.0f, 150);
			value = 1.123456789f;
			NetworkPlayerBlueprint.Instantiate();
		}

		public override void Update()
		{
			if(InputManager.GetKeyDown(Keys.Space))
			{
				Debug.TimeLog(() =>
				{
					int x = 0;
					for (int i = 0; i < 999999999; i++)
						x += i;
					return false;
				});
			}
		}

		private KeyboardInput keyboardInput;
		private void BlueprintSpawnCommand()
		{
			if (keyboardInput == null)
			{
				keyboardInput = new KeyboardInput(InputRestrictions.OnlyCharacters);
				keyboardInput.Begin();
			}
				if (InputManager.GetKeyDown(Keys.Enter))
			{
				string read = keyboardInput.Read();
				Assembly assembly = Assembly.GetExecutingAssembly();
				var types = assembly.GetTypes();

				foreach (var t in types)
				{
					if (t.Name.Contains("Blueprint", StringComparison.CurrentCultureIgnoreCase))
					{
						Debug.Log($"{t.Name} | {read} {t.Name.Contains(read, StringComparison.CurrentCultureIgnoreCase)}");
						if (t.Name.Contains(read, StringComparison.CurrentCultureIgnoreCase))
						{
							var fields = t.GetProperties();
							foreach (var f in fields)
							{
								Debug.Log($"{f.Name}");
							}
							var instance = t.GetProperty("Instance", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null);
							Debug.Log($"YES: {instance}");
							MethodInfo method = t.GetMethod("InstantiatePrefab", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy);
							method.Invoke(instance, new object[] { Vector2.Zero, 0.0f, null });
							break;
						}

					}
				}
			}
		}

		private void OnTweenComplete()
		{
			Debug.Log($"Complete tween");
		}
	}
}