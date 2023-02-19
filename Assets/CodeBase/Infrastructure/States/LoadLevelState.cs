using System;
using System.Threading.Tasks;
using CodeBase.Constants;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.Update;
using CodeBase.Infrastructure.States.Core;
using CodeBase.UI;
using CodeBase.UI.Transition;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : State, IIndependentState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IFactoryService _factoryService;
        private readonly IProgressService _progressService;
        private readonly IUpdateService _updateService;
        private readonly IInputService _inputService;

        private Company _company;
        private BuyWorkPlaceWindow _buyWorkPlaceWindow;

        // private Player _player;

        public LoadLevelState(
            StateMachine stateMachine,
            SceneLoader sceneLoader,
            IFactoryService factoryService,
            IProgressService progressService,
            IUpdateService updateService,
            IInputService inputService
        ) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
            _factoryService = factoryService;
            _progressService = progressService;
            _updateService = updateService;
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
            InitUI();
            InformProgressReaders();
            StateMachine.Enter<GameLoopState>();
        }

        private async Task InitWorld()
        {
            _company = Object.FindObjectOfType<Company>();
            await _company.Construct(_factoryService, _updateService, _inputService);
        }

        private void InitUI()
        {
            _buyWorkPlaceWindow = Object.FindObjectOfType<BuyWorkPlaceWindow>();

            RightTransitionButton rightTransitionButton = Object.FindObjectOfType<RightTransitionButton>();
            LeftTransitionButton leftTransitionButton = Object.FindObjectOfType<LeftTransitionButton>();

            _buyWorkPlaceWindow.BuyButtonClicked += OnBuyButtonClicked;
            
            rightTransitionButton.Clicked += TryToNextWorkSpace;
            leftTransitionButton.Clicked += TryToPreviousWorkSpace;
        }

        private void InformProgressReaders()
        {
            foreach (IProgressReader progressReader in _factoryService.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void OnBuyButtonClicked()
        {
            _buyWorkPlaceWindow.Hide();
            
            if (_company.IsCurrentWorkSpaceLast)
                _company.ToNextWorkSpace();
            else if (_company.IsCurrentWorkSpaceFirst)
                _company.ToPreviousWorkSpace();
            else
                throw new InvalidOperationException("Trying to buy WorkSpace, " +
                                                    "not being on the edges of company");
        }
        
        private void TryToNextWorkSpace()
        {
            if (_company.IsCurrentWorkSpaceLast)
            {
                _buyWorkPlaceWindow.Show();
                return;
            }
            
            _company.ToNextWorkSpace();
        }
        
        private void TryToPreviousWorkSpace()
        {
            if (_company.IsCurrentWorkSpaceFirst)
            {
                _buyWorkPlaceWindow.Show();
                return;
            }
            
            _company.ToPreviousWorkSpace();
        }
    }
}