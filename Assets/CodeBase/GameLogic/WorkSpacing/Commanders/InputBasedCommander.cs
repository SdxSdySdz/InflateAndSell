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
        }

        public override void Enable()
        {
            _inputService.Enable();
        }

        public override void Disable()
        {
            _inputService.Disable();
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_inputService == null)
                return;

            if (_inputService.IsClicked(out ClickTarget clickTarget) && clickTarget != ClickTarget.UI)
                RequestPumping();
        }
    }
}