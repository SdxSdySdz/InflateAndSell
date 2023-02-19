using CodeBase.GameLogic.Marketing;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.Infrastructure.States.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : State, IIndependentState
    {
        private readonly Market _market;

        public GameLoopState(StateMachine stateMachine) : base(stateMachine)
        {
            _market = new Market();
        }

        public async void Enter()
        {
            Company company = Object.FindObjectOfType<Company>();
            await company.StartWork();
        }

        public void Exit()
        {
        }
    }
}