using System;
using System.Threading.Tasks;
using CodeBase.GameLogic.Marketing;
using CodeBase.GameLogic.Upgrading;
using CodeBase.GameLogic.WorkSpacing.Commanders;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.GameLogic.WorkSpacing
{
    public class WorkSpace : MonoBehaviour
    {
        [SerializeField] private Pump _pump;
        [SerializeField] private Hands _hands;
        [SerializeField] private Transform _barrelSpawn;
        
        private IFactoryService _factoryService;
        private Barrel _barrel;
        
        private IPumpingCommander _commander;

        public void Construct(IPumpingCommander commander, IFactoryService factoryService)
        {
            _commander = commander;
            _factoryService = factoryService;
            
            _commander.Settle(this);
        }

        public async Task StartWork()
        {
            _commander.Enable();
            await PrepareNewBarrel();
        }

        public void PumpUp()
        {
            _pump.PumpUp(_commander.Disable, _commander.Enable);
        }

        private async Task PrepareNewBarrel()
        {
            
            if (_barrel != null)
            {
                _barrel.Overflowed -= PickUpBarrel;
                Destroy(_barrel.gameObject);
            }

            _barrel = await _factoryService.CreateBarrel();

            Place(_barrel);
            _barrel.Overflowed += PickUpBarrel;
        }

        private void Place(Barrel barrel)
        {
            barrel.transform.SetParent(_barrelSpawn);
            barrel.transform.localPosition = Vector3.zero;
            
            _pump.Connect(barrel);
        }

        private void PickUpBarrel()
        {
            _hands.PickUp(_barrel, onStart: _commander.Disable, onFinish: OnBarrelPickedUp);
        }

        private async void OnBarrelPickedUp()
        {
            SellCurrentBarrel();
            await PrepareNewBarrel();

            _commander.Enable();
        }

        private void SellCurrentBarrel()
        {
            // _player.Wallet.Add(_market.Sell(_barrel));
        }
    }
}