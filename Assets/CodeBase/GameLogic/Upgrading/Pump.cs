using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public class Pump : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private Hose _hose;
        [SerializeField] private Lever _lever;

        private Barrel _barrel;

        public Hose Hose => _hose;
        public Lever Lever => _lever;

        public void Connect(Barrel barrel)
        {
            _barrel = barrel;
        }
        
        public void Upgrade()
        {
            _hose.Upgrade();
            _lever.Upgrade();
        }

        public void PumpUp(Action onStartPumpingUp, Action onEndPumpingUp)
        {
            _barrel.Fill(_hose.PassingVolume, _lever.PushDuration, onStartPumpingUp, onEndPumpingUp);
        }
    }
}