using System.Threading.Tasks;
using CodeBase.GameLogic;
using CodeBase.GameLogic.SpawnPoints;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : State, IIndependentState
    {
        private readonly IFactoryService _factoryService;
        private readonly IInputService _inputService;

        private Player _player;
        private Barrel _barrel;
        private Hands _hands;
        private BarrelSpawn _barrelSpawn;

        public GameLoopState(
            StateMachine stateMachine, 
            IFactoryService factoryService, 
            IInputService inputService) : base(stateMachine)
        {
            _factoryService = factoryService;
            _inputService = inputService;
        }

        public void Enter()
        {
            InitWorld();
            
            _player = Object.FindObjectOfType<Player>();
            _player.Construct(_inputService);
            
            PrepareNewBarrel();
            
            _hands = Object.FindObjectOfType<Hands>();
        }

        public void Exit()
        {
            
        }

        private void InitWorld()
        {
            _barrelSpawn = Object.FindObjectOfType<BarrelSpawn>();
        }

        private void PickUpBarrel()
        {
            _hands.PickUp(_barrel, onStart: DisableInput, onFinish: PrepareNewBarrel);
        }

        private async void PrepareNewBarrel()
        {
            if (_barrel != null)
            {
                _barrel.Overflowed -= PickUpBarrel;
                Object.Destroy(_barrel.gameObject);
            }

            _barrel = await _factoryService.CreateBarrel(_barrelSpawn.transform.position);
            
            _player.Take(_barrel);
            _barrel.Overflowed += PickUpBarrel;
            
            _inputService.Enable();
        }

        private void DisableInput()
        {
            _inputService.Disable();
        }
    }
}