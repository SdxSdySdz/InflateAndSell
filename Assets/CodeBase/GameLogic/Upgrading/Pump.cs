using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public class Pump : MonoBehaviour, IUpgradeable
    {
        [SerializeField] private Hose _hose;
        [SerializeField] private Lever _lever;

        private Barrel _barrel;
        private Coroutine _pumpingCoroutine;

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
            if (_pumpingCoroutine != null)
                StopCoroutine(_pumpingCoroutine);

            _pumpingCoroutine = StartCoroutine(ProvidePumpingUp(onStart, onFinish));
        }
        
        public IEnumerator ProvidePumpingUp(Action onStart, Action onFinish)
        {
            onStart?.Invoke();
            yield return _lever.PullUp();

            StartCoroutine(_lever.PullDown());
            yield return _barrel.Fill(_hose.PassingVolume, _lever.PushDuration, onEndFilling: onFinish);
        }
    }
}