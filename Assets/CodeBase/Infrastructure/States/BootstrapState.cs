using CodeBase.Constants;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.SDK;
using CodeBase.Infrastructure.States.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : State, IIndependentState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(
            StateMachine stateMachine, 
            SceneLoader sceneLoader, 
            AllServices services
            ) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _services.Get<IYandexGamesService>().Initialize(OnYandexInitialized);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.Register<IYandexGamesService>(new YandexGamesService());
            
            _services.Register<IInputService>(new StandaloneInputService());
            
            _services.Register<IAssetsService>(new AssetsService());
            
            _services.Register<IProgressService>(new ProgressService());

            _services.Register<IFactoryService>(new Factory(
                _services.Get<IAssetsService>(),
                _services.Get<IProgressService>(),
                StateMachine
            ));

            _services.Register<ISaveLoadService>(new SaveLoadService(
                _services.Get<IProgressService>(),
                _services.Get<IFactoryService>()
            ));
        }

        private void OnYandexInitialized()
        {
            Debug.LogError("YandexInit");
            _sceneLoader.Load(Scenes.Main, LoadLevel);
        }
        
        private void LoadLevel()
        {
            StateMachine.Enter<LoadProgressState>();
        }
    }
}