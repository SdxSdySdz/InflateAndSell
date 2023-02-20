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

        public void Update(float deltaTime)
        {
            if (_workSpace == null)
                return;

            OnUpdate(deltaTime);
        }

        protected void RequestPumping(Action onStart = null, Action onFinish = null)
        {
            _workSpace.PumpUp(onStart, onFinish);
        }

        protected abstract void OnUpdate(float deltaTime);
    }
}