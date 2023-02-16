using CodeBase.GameLogic;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States.Core;
using UnityEngine;

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
            _inputService.Enable();
            
            _player = Object.FindObjectOfType<Player>();
            _player.Construct(_inputService);
            
            PrepareNewBarrel();
            
            _hands = Object.FindObjectOfType<Hands>();
        }

        public void Exit()
        {
            
        }

        private void PickUpBarrel()
        {
            _hands.PickUp(_barrel, onStart: DisableInput, onFinish: PrepareNewBarrel);
        }

        private void PrepareNewBarrel()
        {
            if (_barrel != null)
                _barrel.Overflowed -= PickUpBarrel;
            
            _barrel = Object.FindObjectOfType<Barrel>();
            _barrel.Construct(new Capacity(5));
            
            _player.Take(_barrel);
            _barrel.Overflowed += PickUpBarrel;
        }

        private void DisableInput()
        {
            _inputService.Disable();
        }
    }
}