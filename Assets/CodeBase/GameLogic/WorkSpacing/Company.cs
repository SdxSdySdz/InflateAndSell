using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.GameLogic.WorkSpacing.Commanders;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Update;
using UnityEngine;

namespace CodeBase.GameLogic.WorkSpacing
{
    [RequireComponent(typeof(RowPlacer))]
    public class Company : MonoBehaviour
    {
        private RowPlacer _placer;
        private IFactoryService _factoryService;
        private IUpdateService _updateService;
        private int _currentIndex;

        private Vector3 _currentWorkSpaceOrigin;
        
        public bool IsCurrentWorkSpaceLast => _currentIndex == _placer.Count - 1;
        public bool IsCurrentWorkSpaceFirst => _currentIndex == 0;

        private void Awake()
        {
            _placer = GetComponent<RowPlacer>();
            _currentWorkSpaceOrigin = transform.position;
        }

        public async Task Construct(
            IFactoryService factoryService, 
            IUpdateService updateService, 
            IInputService inputService
            )
        {
            _factoryService = factoryService;
            _updateService = updateService;

            List<WorkSpace> workSpaces = await CreateWorkspaces(inputService);
            foreach (var workSpace in workSpaces)
            {
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
                    new EmployeeCommander(2f, _updateService), 
                    Vector3.zero, 
                    180);
                await workSpace.StartWork();
                add.Invoke(workSpace);
            }
            else 
                _currentIndex -= indexOffset;
            
            _currentIndex += 1 - indexOffset;
    
            _placer.Focus(_currentIndex, _currentWorkSpaceOrigin);
        }

        private async Task<List<WorkSpace>> CreateWorkspaces(IInputService inputService)
        {
            List<WorkSpace> workSpaces = new List<WorkSpace>()
            {
                await _factoryService.CreateWorkPlace(
                    new InputBasedCommander(_updateService, inputService), 
                    transform.position, 
                    180
                    ),
            };

            return workSpaces;
        }

        private WorkSpace GetWorkSpace(int index)
        {
            return _placer.Get(index).GetComponent<WorkSpace>();
        }
    }
}