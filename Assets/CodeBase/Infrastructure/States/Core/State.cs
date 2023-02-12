namespace CodeBase.Infrastructure.States.Core
{
    public abstract class State
    {
        protected State(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected StateMachine StateMachine { get; }
    }
}