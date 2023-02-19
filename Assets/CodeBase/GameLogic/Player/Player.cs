using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.GameLogic.WorkSpacing.Commanders;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.Update;
using UnityEngine;

namespace CodeBase.GameLogic.Player
{
    public class Player : MonoBehaviour, IProgressReader, IProgressWriter
    { 
        private Wallet _wallet;
        private LinkedList<WorkSpace> _workSpaces;
        private InputBasedCommander _commander;

        public Wallet Wallet => _wallet;
        
        public void Construct(
            Wallet wallet, 
            IEnumerable<WorkSpace> workSpaces, 
            IInputService inputService, 
            IUpdateService updateService
            )
        {
            _wallet = wallet;
            _workSpaces = new LinkedList<WorkSpace>(workSpaces);
            _commander = new InputBasedCommander(inputService);
            
            _workSpaces.First.Value.Accept(_commander);
            
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
            _workSpaces.First.Value.Place(barrel);
        }
    }
}