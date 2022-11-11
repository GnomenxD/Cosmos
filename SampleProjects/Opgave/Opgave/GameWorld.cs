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
		public override void Initialize()
		{
		}
		public override void Start()
		{
			GameObject go = new GameObject();
			SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
			sr.Sprite = Assets.PlayerShip1Blue;

			go.AddComponent<Script>();
		}

		public override void Update()
		{
		}
	}

	public enum ColourState
	{
		Blue,
		Green,
		Orange,
		Red,
	}

	public class Script : GameBehaviour
	{
		private State<ColourState, GraphicsState> stateMachine = new State<ColourState, GraphicsState>();
		private SpriteRenderer spriteRenderer;

		protected override void Start()
		{
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			stateMachine.AddState(ColourState.Blue, new GraphicsState(Assets.PlayerShip1Blue, sr));
			stateMachine.AddState(ColourState.Green, new GraphicsState(Assets.PlayerShip1Green, sr));
			stateMachine.AddState(ColourState.Orange, new GraphicsState(Assets.PlayerShip1Orange, sr));
			stateMachine.AddState(ColourState.Red, new GraphicsState(Assets.PlayerShip1Red, sr));
		}

		protected override void OnEnable()
		{
			Input.AddInputAction(100, "ChangeColour", started: ChangeColour, new InputControl[]
			{
				new InputControl(Keys.D1, Interaction.Press, ColourState.Blue),
				new InputControl(Keys.D2, Interaction.Press, ColourState.Green),
				new InputControl(Keys.D3, Interaction.Press, ColourState.Orange),
				new InputControl(Keys.D4, Interaction.Press, ColourState.Red)
			});
		}

		private void ChangeColour(CallbackContext callback)
		{
			stateMachine.Transition(callback.ReadValue<ColourState>());
		}

		protected override void Update()
		{
		}
	}

	public class GraphicsState : IState
	{
		private Sprite sprite;
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => sprite;

		public GraphicsState(Sprite sprite, SpriteRenderer spriteRenderer)
		{
			this.sprite = sprite;
			this.spriteRenderer = spriteRenderer;
		}

		public void Enter()
		{
			spriteRenderer.Sprite = sprite;
		}

		public void Exit()
		{
		}

		public void Transition()
		{
		}
	}
}