using System.Threading.Tasks;
using CodeBase.Constants;
using CodeBase.GameLogic.Marketing;
using CodeBase.GameLogic.Player;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
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
        private readonly ISaveLoadService _saveLoadService;

        private Market _market;
        private Company _company;
        private BuyWorkPlaceWindow _buyWorkPlaceWindow;
        private Wallet _wallet;

        public LoadLevelState(
            StateMachine stateMachine,
            SceneLoader sceneLoader,
            IFactoryService factoryService,
            IProgressService progressService,
            IUpdateService updateService,
            IInputService inputService,
            ISaveLoadService saveLoadService
        ) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
            _factoryService = factoryService;
            _progressService = progressService;
            _updateService = updateService;
            _inputService = inputService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _factoryService.Cleanup();
            _factoryService.WarmUp();
            _sceneLoader.Load(Scenes.GameLoop, EnterGameLoop);
            
            _market = _factoryService.CreateMarket();
        }

        public void Exit()
        {
        }

        private async void EnterGameLoop()
        {
            await InitWorld();
            InitUI();
            InformProgressReaders();
            
            await _company.CreateWorkSpaces();
            
            StateMachine.Enter<GameLoopState>();
        }

        private async Task InitWorld()
        {
            _company = Object.FindObjectOfType<Company>();

            _wallet = _factoryService.CreateWallet();
            
            WalletPresenter walletPresenter = Object.FindObjectOfType<WalletPresenter>();
            walletPresenter.Construct(_wallet);
            
            _company.Construct(_market, _wallet, _factoryService, _updateService, _inputService, _saveLoadService);
            _factoryService.ProgressReaders.Add(_company);
            _factoryService.ProgressWriters.Add(_company);
        }

        private void InitUI()
        {
            _buyWorkPlaceWindow = Object.FindObjectOfType<BuyWorkPlaceWindow>();
            _buyWorkPlaceWindow.Construct(_company, _market, _wallet);

            RightTransitionButton rightTransitionButton = Object.FindObjectOfType<RightTransitionButton>();
            LeftTransitionButton leftTransitionButton = Object.FindObjectOfType<LeftTransitionButton>();
            
            rightTransitionButton.Construct(_company, _buyWorkPlaceWindow);
            leftTransitionButton.Construct(_company, _buyWorkPlaceWindow);
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