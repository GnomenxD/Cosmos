namespace CosmosEngine
{
	public interface IState
	{
		void Enter();
		void Exit();
		void Transition();

	}
}