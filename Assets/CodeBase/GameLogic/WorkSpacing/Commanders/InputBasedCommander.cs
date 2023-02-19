using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Update;

namespace CodeBase.GameLogic.WorkSpacing.Commanders
{
    public class InputBasedCommander : IPumpingCommander, IUpdatable
    {
        private readonly IInputService _inputService;
        
        private WorkSpace _workSpace;
        
        public InputBasedCommander(IUpdateService updateService, IInputService inputService)
        {
            updateService.Register(this);
            
            _inputService = inputService;
            EnableInput();
        }
        
        public void Update(float deltaTime)
        {
            if (_inputService == null || _workSpace == null)
                return;

            if (_inputService.IsClicked(out ClickTarget clickTarget) && clickTarget != ClickTarget.UI)
                RequestPumping();
        }

        public void Settle(WorkSpace workSpace)
        {
            _workSpace = workSpace;
        }

        private void EnableInput()
        {
            _inputService.Enable();
        }

        private void DisableInput()
        {
            _inputService.Disable();
        }

        private void RequestPumping()
        {
            _workSpace.PumpUp(onStart: DisableInput,  onFinish: EnableInput);
        }
    }
}