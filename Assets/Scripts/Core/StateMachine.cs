public class StateMachine
{
    public EntityState CurrentState { get; private set; }

    public void Initialize(EntityState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
