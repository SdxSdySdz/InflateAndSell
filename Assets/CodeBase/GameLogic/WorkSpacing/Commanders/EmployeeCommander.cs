using CodeBase.Infrastructure.Services.Update;

namespace CodeBase.GameLogic.WorkSpacing.Commanders
{
    public class EmployeeCommander : UpdatableCommander
    {
        private readonly float _pumpingDelay;
        
        private float _time;
        
        public EmployeeCommander(float pumpingDelay, IUpdateService updateService) : base(updateService)
        {
            _pumpingDelay = pumpingDelay;
            _time = 0;
        }

        protected override void OnUpdate(float deltaTime)
        {
            _time += deltaTime;
            if (_time >= _pumpingDelay)
            {
                RequestPumping();
                _time = 0;
            }
        }
    }
}