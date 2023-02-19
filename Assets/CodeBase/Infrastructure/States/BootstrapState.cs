using CodeBase.Constants;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.SDK;
using CodeBase.Infrastructure.Services.Update;
using CodeBase.Infrastructure.States.Core;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : State, IIndependentState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(StateMachine stateMachine,
            SceneLoader sceneLoader,
            AllServices services, 
            IUpdateService updateService) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices(updateService);
        }

        public void Enter()
        {
            _services.Get<IYandexGamesService>().Initialize(OnYandexInitialized);
        }

        public void Exit()
        {
        }

        private void RegisterServices(IUpdateService updateService)
        {
            _services.Register<IYandexGamesService>(new YandexGamesService());
            
            _services.Register<IInputService>(new StandaloneInputService());

            _services.Register<IUpdateService>(updateService);
            
            _services.Register<IAssetsService>(new AssetsService());

            _services.Register<IFactoryService>(new Factory(
                _services.Get<IAssetsService>(),
                _services.Get<IUpdateService>(),
                _services.Get<IInputService>()
                ));
            
            _services.Register<IProgressService>(new ProgressService());

            _services.Register<ISaveLoadService>(new SaveLoadService(
                _services.Get<IProgressService>(),
                _services.Get<IFactoryService>()
            ));
        }

        private void OnYandexInitialized()
        {
            _sceneLoader.Load(Scenes.Main, LoadLevel);
        }
        
        private void LoadLevel()
        {
            StateMachine.Enter<LoadProgressState>();
        }
    }
}