public class StateMachine
{
    public State CurrentState { get; set; }

    public void Initialize(State startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
