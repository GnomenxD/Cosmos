namespace CosmosEngine
{
	public class State<TKey, TState> where TKey : notnull where TState : IState
	{
		private readonly Dictionary<TKey, TState> stateMachine;
		private TState currentState;
		private TState defaultState;

		public State()
		{
			stateMachine = new Dictionary<TKey, TState>();
		}

		public void Transition(TKey key)
		{
			TransitionTo(stateMachine[key]);
		}

		private void TransitionTo(TState state)
		{
			if (currentState != null)
			{
				if (currentState.Equals(state))
				{
					return;
				}
				currentState.Exit();
			}
			currentState = state;
			currentState.Transition();
			currentState.Enter();
		}

		public void AddState(TKey key, TState state, bool isDefault = false)
		{
			if (stateMachine.Count == 0)
			{
				TransitionTo(state);
				defaultState = state;
			}
			if (isDefault)
			{
				defaultState = state;
			}
			stateMachine.Add(key, state);
		}

		public TState CurrentState()
		{
			return currentState;
		}
	}
}