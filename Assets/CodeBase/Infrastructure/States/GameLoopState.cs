using CodeBase.Infrastructure.States.Core;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : State, IIndependentState
    {
        public GameLoopState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}