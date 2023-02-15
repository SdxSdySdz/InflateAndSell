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
        
        public Pump Pump => _pump;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (_inputService == null)
                return;

            if (_inputService.IsClicked(out ClickTarget clickTarget))
            {
                if (clickTarget == ClickTarget.UI)
                    return;
                    
                _pump.PumpUp();
            }
        }

        public void Take(Barrel barrel)
        {
            _pump.Connect(barrel);
        }
    }
}