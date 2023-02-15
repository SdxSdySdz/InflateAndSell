using CodeBase.GameLogic;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States.Core;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : State, IIndependentState
    {
        private readonly IInputService _inputService;

        private Player _player;
        
        public GameLoopState(StateMachine stateMachine, IInputService inputService) : base(stateMachine)
        {
            _inputService = inputService;
        }

        public void Enter()
        {
            _player = Object.FindObjectOfType<Player>();
            _player.Construct(_inputService);
            
            Barrel barrel = Object.FindObjectOfType<Barrel>();
            barrel.Construct(new Capacity(5));

            _player.Take(barrel);
        }

        public void Exit()
        {
            
        }
    }
}