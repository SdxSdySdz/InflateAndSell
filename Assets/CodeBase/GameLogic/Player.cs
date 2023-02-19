using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.GameLogic.WorkSpacing.Commanders;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.Update;
using UnityEngine;

namespace CodeBase.GameLogic
{
    public class Player : MonoBehaviour, IProgressReader, IProgressWriter
    { 
        private Wallet _wallet;
        private List<WorkSpace> _workSpaces;
        private InputBasedCommander _commander;

        public Wallet Wallet => _wallet;
        
        public void Construct(
            Wallet wallet, 
            List<WorkSpace> workSpaces, 
            IInputService inputService, 
            IUpdateService updateService
            )
        {
            _wallet = wallet;
            _workSpaces = workSpaces;
            _commander = new InputBasedCommander(inputService);
            
            _workSpaces[0].Accept(_commander);
            
            updateService.Register(_commander);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _wallet.Construct(progress.Wallet.Amount);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.Wallet.Amount = _wallet.Amount;
        }

        public void Take(Barrel barrel)
        {
            _workSpaces[0].Place(barrel);
        }
    }
}