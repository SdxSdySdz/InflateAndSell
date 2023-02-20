using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.GameLogic.WorkSpacing
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] private Transform _model;
        [SerializeField] private Vector3 _startScale;
        [SerializeField] private Vector3 _endScale;

        [Header("Handles")] 
        [SerializeField] private Transform _leftHandle;
        [SerializeField] private Transform _rightHandle;
        
        private Capacity _capacity;

        public Transform LeftHandlePosition => _leftHandle;
        public Transform RightHandlePosition => _rightHandle;
        
        public event UnityAction Overflowed;
        
        public void Construct(Capacity capacity)
        {
            _capacity = capacity;
            _model.localScale = _startScale;
        }

        public IEnumerator Fill(float volume, float pushDuration, Action onStartFilling = null, Action onEndFilling = null)
        {
            _capacity.Fill(volume);
            yield return StartCoroutine(
                Inflate(
                        _capacity.FillingRatio,
                        pushDuration, 
                        onStartFilling, 
                        onEndFilling
                    ));
        }

        private IEnumerator Inflate(float fillingRatio, float duration, Action onStartFilling, Action onEndFilling)
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();
            
            Vector3 startScale = _model.localScale;
            Vector3 targetScale = Vector3.Lerp(_startScale, _endScale, fillingRatio);

            float time = 0;
            onStartFilling?.Invoke();
            while (time < duration)
            {
                _model.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
                
                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }

            _model.localScale = targetScale;
            onEndFilling?.Invoke();
            
            if (_capacity.IsOverflowed)
                Overflowed?.Invoke();
        }
    }
}