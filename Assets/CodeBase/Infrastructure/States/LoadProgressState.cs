using CodeBase.Infrastructure.States.Core;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : State, IIndependentState
    {
        public LoadProgressState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public void Enter()
        {
            StateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
        }
    }
}