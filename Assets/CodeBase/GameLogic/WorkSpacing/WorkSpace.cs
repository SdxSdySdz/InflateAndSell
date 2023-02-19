using System;
using CodeBase.GameLogic.Upgrading;
using CodeBase.GameLogic.WorkSpacing.Commanders;
using UnityEngine;

namespace CodeBase.GameLogic.WorkSpacing
{
    public class WorkSpace : MonoBehaviour
    {
        [SerializeField] private Pump _pump;
        
        private IPumpingCommander _commander;
        
        public void Place(Barrel barrel)
        {
            _pump.Connect(barrel);
        }

        public void PumpUp(Action onStart = null, Action onFinish = null)
        {
            _pump.PumpUp(onStart, onFinish);
        }

        public void Accept(IPumpingCommander commander)
        {
            _commander = commander;
            _commander.Settle(this);
        }
    }
}