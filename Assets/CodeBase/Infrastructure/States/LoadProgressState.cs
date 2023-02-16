using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States.Core;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : State, IIndependentState
    {
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;

        public LoadProgressState(
            StateMachine stateMachine, 
            IProgressService progressService, 
            ISaveLoadService saveLoadProgress) : base(stateMachine)
        {
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
      
            StateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = 
                _saveLoadProgress.LoadProgress() 
                ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress();
            return progress;
        }
    }
}