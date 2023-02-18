using System.Collections;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public class Lever : MultiplierBasedUpgradeable
    {
        [SerializeField] private Transform _model;
        [SerializeField] private float _pumpingOffset;
        
        public float PushDuration => CurrentValue;
        
        protected override float BaseValue => 0.35f;
        protected override float Multiplier => 0.89f;

        public IEnumerator PullUp()
        {
            return Move(_pumpingOffset, PushDuration);
        }
        
        public IEnumerator PullDown()
        {
            return Move(-_pumpingOffset, PushDuration);
        }

        protected override int ReadLevel(PlayerProgress progress)
        {
            return progress.Lever.Level;
        }

        protected override void WriteLevel(PlayerProgress progress, int level)
        {
            progress.Lever.Level = level;
        }

        private IEnumerator Move(float offset, float duration)
        {
            Vector3 endPosition = _model.transform.position + Vector3.up * offset;
            
            yield return StartCoroutine(Move(endPosition, PushDuration));
        }
        
        private IEnumerator Move(Vector3 endPosition, float duration)
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();
            Vector3 startPosition = _model.transform.position;
            float time = 0;
        
            while (time < duration)
            {
                _model.transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
                
                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            
            _model.transform.position = endPosition;
        }
    }
}