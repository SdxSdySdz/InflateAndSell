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
        private readonly Market _market;

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
            _market = new Market();
        }

        public void Enter()
        {
            _player = Object.FindObjectOfType<Player>();
            _hands = Object.FindObjectOfType<Hands>();
            _barrelSpawn = Object.FindObjectOfType<BarrelSpawn>();
            
            PrepareNewBarrel();
            _inputService.Enable();
        }

        public void Exit()
        {
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
        }
        
        private void PickUpBarrel()
        {
            _hands.PickUp(_barrel, onStart: DisableInput, onFinish: OnBarrelPickedUp);
        }
        
        private void DisableInput()
        {
            _inputService.Disable();
        }

        private void OnBarrelPickedUp()
        {
            SellCurrentBarrel();
            PrepareNewBarrel();
            _inputService.Enable();
        }

        private void SellCurrentBarrel()
        {
            _player.Wallet.Add(_market.Sell(_barrel));
        }
    }
}