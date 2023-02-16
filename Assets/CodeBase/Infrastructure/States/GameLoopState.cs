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
        private Barrel _barrel;
        private Hands _hands;

        public GameLoopState(StateMachine stateMachine, IInputService inputService) : base(stateMachine)
        {
            _inputService = inputService;
        }

        public void Enter()
        {
            _player = Object.FindObjectOfType<Player>();
            _player.Construct(_inputService);
            
            _barrel = Object.FindObjectOfType<Barrel>();
            _barrel.Construct(new Capacity(5));
            
            _hands = Object.FindObjectOfType<Hands>();

            
            _player.Take(_barrel);

            _barrel.Overflowed += PickUpBarrel;
        }

        public void Exit()
        {
            
        }

        private void PickUpBarrel()
        {
            _hands.PickUp(_barrel);
        }
    }
}