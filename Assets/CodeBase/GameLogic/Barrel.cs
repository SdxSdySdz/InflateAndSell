using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.GameLogic
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
        private Coroutine _fillingCoroutine;

        public Vector3 LeftHandlePosition => _leftHandle.position;
        public Vector3 RightHandlePosition => _rightHandle.position;
        
        public event UnityAction Overflowed;
        
        public void Construct(Capacity capacity)
        {
            _capacity = capacity;
            _model.localScale = _startScale;
        }

        public void Fill(float volume, float pushDuration, Action onStartFilling = null, Action onEndFilling = null)
        {
            if (_fillingCoroutine != null)
                StopCoroutine(_fillingCoroutine);
            
            _capacity.Fill(volume);
            _fillingCoroutine = StartCoroutine(
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