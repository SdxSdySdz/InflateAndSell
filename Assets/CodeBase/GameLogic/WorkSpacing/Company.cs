using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.GameLogic.Marketing;
using CodeBase.GameLogic.Player;
using CodeBase.GameLogic.WorkSpacing.Commanders;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.Update;
using UnityEngine;

namespace CodeBase.GameLogic.WorkSpacing
{
    [RequireComponent(typeof(RowPlacer))]
    public class Company : MonoBehaviour, IProgressReader, IProgressWriter
    {
        private Market _market;
        private Wallet _wallet;
        private RowPlacer _placer;
        private IFactoryService _factoryService;
        private IUpdateService _updateService;
        private IInputService _inputService;
        private ISaveLoadService _saveLoadService;
        private int _currentIndex;

        private List<WorkSpaceProgress> _workSpaceProgresses;
        private Vector3 _currentWorkSpaceOrigin;
        
        public bool IsCurrentWorkSpaceLast => _currentIndex == _placer.Count - 1;
        public bool IsCurrentWorkSpaceFirst => _currentIndex == 0;
        public bool CanBuyWorkSpace => _wallet.Amount >= _market.GetWorkSpaceCost();

        private void Awake()
        {
            _placer = GetComponent<RowPlacer>();
            _currentWorkSpaceOrigin = transform.position;
        }

        public void Construct(
            Market market, 
            Wallet wallet,
            IFactoryService factoryService,
            IUpdateService updateService,
            IInputService inputService,
            ISaveLoadService saveLoadService)
        {
            _market = market;
            _wallet = wallet;
            _factoryService = factoryService;
            _updateService = updateService;
            _inputService = inputService;
            _saveLoadService = saveLoadService;
        }

        private void OnEnable()
        {
            if (_wallet != null)
                _wallet.Changed += SaveProgress;
        }
        
        private void OnDisable()
        {
            if (_wallet != null)
                _wallet.Changed -= SaveProgress;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _workSpaceProgresses = new List<WorkSpaceProgress>();
            foreach (WorkSpaceProgress workSpaceProgress in progress.Company.WorkSpaces)
            {
                _workSpaceProgresses.Add(workSpaceProgress);
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            var workSpaceProgresses = new List<WorkSpaceProgress>();
            for (int i = 0; i < _placer.Count; i++)
            {
                WorkSpace workSpace = GetWorkSpace(i);
                workSpaceProgresses.Add(workSpace.ToProgressData());
            }

            progress.Company.WorkSpaces = workSpaceProgresses;
        }

        public async Task CreateWorkSpaces()
        {
            if (_workSpaceProgresses.Count == 0)
            {
                IPumpingCommander commander = new InputBasedCommander(_updateService, _inputService);
                WorkSpace workSpace = await _factoryService.CreateWorkPlace(commander, _market, _wallet, Vector3.zero, 180);
                
                _placer.AddLast(workSpace.transform);
                return;
            }

            foreach (WorkSpaceProgress workSpaceProgress in _workSpaceProgresses)
            {
                IPumpingCommander commander = ParseCommander(workSpaceProgress);
                WorkSpace workSpace = await _factoryService.CreateWorkPlace(commander, _market, _wallet, Vector3.zero, 180);
                
                _placer.AddLast(workSpace.transform);
            }
        }

        public async Task StartWork()
        {
            for (int i = 0; i < _placer.Count; i++)
            {
                await GetWorkSpace(i).StartWork();
            }
        }

        public async void ToNextWorkSpace()
        {
            await ToOtherWorkSpace(
                isBoundary: IsCurrentWorkSpaceLast, 
                isIndexOffsetNeeded: false, 
                add: ((workSpace) => _placer.AddLast(workSpace.transform)));
        }

        public async void ToPreviousWorkSpace()
        {
            await ToOtherWorkSpace(
                isBoundary: IsCurrentWorkSpaceFirst, 
                isIndexOffsetNeeded: true, 
                add: ((workSpace) => _placer.AddFirst(workSpace.transform)));
        }
        
        private async Task ToOtherWorkSpace(bool isBoundary, bool isIndexOffsetNeeded, Action<WorkSpace> add)
        {
            int indexOffset = isIndexOffsetNeeded ? 1 : 0;
            if (isBoundary)
            {
                WorkSpace workSpace = await _factoryService.CreateWorkPlace(
                    new EmployeeCommander(_updateService),
                    _market,
                    _wallet,
                    Vector3.zero, 
                    180);
                await workSpace.StartWork();
                add.Invoke(workSpace);
                
                SaveProgress();
            }
            else 
                _currentIndex -= indexOffset;
            
            _currentIndex += 1 - indexOffset;
    
            _placer.Focus(_currentIndex, _currentWorkSpaceOrigin);
        }
        
        private void SaveProgress(int _ = default)
        {
            _saveLoadService.SaveProgress();
        }

        private IPumpingCommander ParseCommander(WorkSpaceProgress progress)
        {
            switch (progress.CommanderType)
            {
                case PumpingCommanderType.InputBased:
                    return new InputBasedCommander(_updateService, _inputService);
                
                case PumpingCommanderType.Employee:
                    return new EmployeeCommander(_updateService);
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(progress));
            }
        }
        
        private WorkSpace GetWorkSpace(int index)
        {
            return _placer.Get(index).GetComponent<WorkSpace>();
        }

        public void SpentMoney(int amount)
        {
            _wallet.Spent(amount);
        }
    }
}