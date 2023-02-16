using CodeBase.GameLogic.Upgrading;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

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

        private void DisableInput()
        {
            _inputService.Disable();
        }

        private void EnableInput()
        {
            _inputService.Enable();
        }
    }
}