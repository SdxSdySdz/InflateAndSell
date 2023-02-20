using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Update;

namespace CodeBase.GameLogic.WorkSpacing.Commanders
{
    public class InputBasedCommander : UpdatableCommander
    {
        private readonly IInputService _inputService;
        
        public InputBasedCommander(IUpdateService updateService, IInputService inputService) : base(updateService)
        {
            _inputService = inputService;
            EnableInput();
        }
        
        protected override void OnUpdate(float deltaTime)
        {
            if (_inputService == null)
                return;

            if (_inputService.IsClicked(out ClickTarget clickTarget) && clickTarget != ClickTarget.UI)
                RequestPumping(DisableInput, EnableInput);
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