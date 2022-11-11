using CosmosEngine;
using CosmosEngine.Collections;
using CosmosEngine.CoreModule;
using CosmosEngine.InputModule;
using System.Collections;
using System.Collections.Generic;

namespace Opgave
{
	public class GameWorld : Game
	{
		private KeyboardInput keyboardInput;
		public override void Initialize()
		{
			keyboardInput = new KeyboardInput();
		}
		public override void Start()
		{
			GameObject go = new GameObject();
			go.AddComponent<Script>();
		}

		public override void Update()
		{
			string s = keyboardInput.ToString();
		}
	}

	public class Script : GameBehaviour
	{
		protected override void OnDrawGizmos()
		{
			Gizmos.DrawLine(Transform.Position, Camera.Main.ScreenToWorld(InputManager.MousePosition), Colour.Lime);
		}
	}
}