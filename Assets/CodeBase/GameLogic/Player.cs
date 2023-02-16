using System;
using CodeBase.GameLogic.Upgrading;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.GameLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Pump _pump;
        
        private IInputService _inputService;
        private bool _isInputEnabled;
        
        public Pump Pump => _pump;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            EnableInput();
        }

        private void Update()
        {
            if (_inputService == null || _isInputEnabled == false)
                return;

            if (_inputService.IsClicked(out ClickTarget clickTarget) && clickTarget != ClickTarget.UI)
                _pump.PumpUp(onStart: DisableInput, onFinish: EnableInput);
        }

        public void Take(Barrel barrel)
        {
            _pump.Connect(barrel);
        }

        private void DisableInput()
        {
            _isInputEnabled = false;
        }

        private void EnableInput()
        {
            _isInputEnabled = true;
        }
    }
}