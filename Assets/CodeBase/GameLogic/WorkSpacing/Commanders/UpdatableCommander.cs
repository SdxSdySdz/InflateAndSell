using System;
using CodeBase.Infrastructure.Services.Update;

namespace CodeBase.GameLogic.WorkSpacing.Commanders
{
    public abstract class UpdatableCommander : IPumpingCommander, IUpdatable
    {
        private WorkSpace _workSpace;

        protected UpdatableCommander(IUpdateService updateService)
        {
            updateService.Register(this);
        }
        
        public void Settle(WorkSpace workSpace)
        {
            _workSpace = workSpace;
        }

        public abstract void Enable();

        public abstract void Disable();
        

        public void Update(float deltaTime)
        {
            if (_workSpace == null)
                return;

            OnUpdate(deltaTime);
        }
        
        protected abstract void OnUpdate(float deltaTime);
        
        protected void RequestPumping()
        {
            _workSpace.PumpUp();
        }
    }
}