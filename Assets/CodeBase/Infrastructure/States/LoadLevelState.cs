using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Constants;
using CodeBase.GameLogic;
using CodeBase.GameLogic.Player;
using CodeBase.GameLogic.SpawnPoints;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.States.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : State, IIndependentState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IFactoryService _factoryService;
        private readonly IProgressService _progressService;
        private readonly IInputService _inputService;

        public LoadLevelState(
            StateMachine stateMachine,
            SceneLoader sceneLoader,
            IFactoryService factoryService,
            IProgressService progressService,
            IInputService inputService
        ) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
            _factoryService = factoryService;
            _progressService = progressService;
            _inputService = inputService;
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
            await InitWorld();
            InformProgressReaders();
            StateMachine.Enter<GameLoopState>();
        }

        private async Task InitWorld()
        {
            List<WorkSpace> workSpaces = await CreateWorkspaces();
            await InitPlayer(workSpaces);
        }

        private async Task<List<WorkSpace>> CreateWorkspaces()
        {
            WorkSpaceSpawn spawn = Object.FindObjectOfType<WorkSpaceSpawn>();
            
            List<WorkSpace> workSpaces = new List<WorkSpace>()
            {
                await _factoryService.CreateWorkPlace(spawn.transform.position, 180),
            };

            return workSpaces;
        }

        private async Task InitPlayer(List<WorkSpace> workSpaces)
        {
            Wallet wallet = Object.FindObjectOfType<Wallet>();
            await _factoryService.CreatePlayer(wallet, workSpaces, _inputService);
        }

        private void InformProgressReaders()
        {
            foreach (IProgressReader progressReader in _factoryService.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }
    }
}