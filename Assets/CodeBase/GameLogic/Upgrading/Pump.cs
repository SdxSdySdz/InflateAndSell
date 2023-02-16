using System;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public class Pump : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private Hose _hose;
        [SerializeField] private Lever _lever;

        private Barrel _barrel;

        public void Connect(Barrel barrel)
        {
            _barrel = barrel;
        }
        
        public void Upgrade()
        {
            _hose.Upgrade();
            _lever.Upgrade();
        }

        public void PumpUp(Action onStart, Action onFinish)
        {
            _barrel.Fill(_hose.PassingVolume, _lever.PushDuration, onStart, onFinish);
        }
    }
}