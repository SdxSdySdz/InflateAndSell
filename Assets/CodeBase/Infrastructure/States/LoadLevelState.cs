using CodeBase.Constants;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.States.Core;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : State, IIndependentState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IFactoryService _factoryService;
        private readonly IProgressService _progressService;

        public LoadLevelState(
            StateMachine stateMachine, 
            SceneLoader sceneLoader,
            IFactoryService factoryService,
            IProgressService progressService
            ) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
            _factoryService = factoryService;
            _progressService = progressService;
        }

        public void Enter()
        {
            _factoryService.Cleanup();
            _factoryService.WarmUp();
            _sceneLoader.Load(Scenes.GameLoop, EnterGameLoop);
        }

        public void Exit()
        {
            
        }

        private async void EnterGameLoop()
        {
            // await InitUIRoot();
            // await InitGameWorld();
            InformProgressReaders();
            StateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (IProgressReader progressReader in _factoryService.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }
    }
}