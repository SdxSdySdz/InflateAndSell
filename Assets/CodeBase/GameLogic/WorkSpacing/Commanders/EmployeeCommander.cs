using CodeBase.Infrastructure.Services.Update;

namespace CodeBase.GameLogic.WorkSpacing.Commanders
{
    public class EmployeeCommander : UpdatableCommander
    {
        private readonly float _pumpingDelay;

        private bool _isEnabled;
        private float _time;

        public EmployeeCommander(IUpdateService updateService) : base(updateService)
        {
            _pumpingDelay = UnityEngine.Random.Range(0.5f, 3f);
            _time = 0;
        }

        public override void Enable()
        {
            _isEnabled = true;
        }

        public override void Disable()
        {
            _isEnabled = false;
        }

        protected override void OnUpdate(float deltaTime)
        {
            _time += deltaTime;
            if (_time >= _pumpingDelay)
            {
                if (_isEnabled)
                    RequestPumping();
                
                _time = 0;
            }
        }
    }
}