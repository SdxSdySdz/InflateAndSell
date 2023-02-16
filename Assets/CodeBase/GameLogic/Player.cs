using CodeBase.Data;
using CodeBase.GameLogic.Upgrading;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.GameLogic
{
    public class Player : MonoBehaviour, IProgressReader, IProgressWriter
    {
        private Wallet _wallet;
        private Pump _pump;
        private IInputService _inputService;
        private ISaveLoadService _saveLoadService;

        public Wallet Wallet => _wallet;
        
        public void Construct(Wallet wallet, Pump pump, IInputService inputService)
        {
            _wallet = wallet;
            _pump = pump;
            _inputService = inputService;

            EnableInput();
        }

        private void Update()
        {
            if (_inputService == null)
                return;

            if (_inputService.IsClicked(out ClickTarget clickTarget) && clickTarget != ClickTarget.UI)
                _pump.PumpUp(onStart: DisableInput, onFinish: EnableInput);
        }

        public void Take(Barrel barrel)
        {
            _pump.Connect(barrel);
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _wallet.Construct(progress.Wallet.Amount);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.Wallet.Amount = _wallet.Amount;
        }

        private void EnableInput()
        {
            _inputService.Enable();
        }

        private void DisableInput()
        {
            _inputService.Disable();
        }
    }
}