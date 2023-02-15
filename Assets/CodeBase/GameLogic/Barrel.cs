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
        
        private Capacity _capacity;
        private Coroutine _fillingCoroutine;

        public event UnityAction Overflowed;
        
        public void Construct(Capacity capacity)
        {
            _capacity = capacity;
            _model.localScale = _startScale;

            _capacity.Overflowed += OnOverflowed;
        }

        public void Fill(float volume, float pushDuration)
        {
            _capacity.Fill(volume);

            ProvideInflation(pushDuration);
        }

        private void OnOverflowed()
        {
            Overflowed?.Invoke();
        }

        private void ProvideInflation(float duration)
        {
            if (_fillingCoroutine != null)
                StopCoroutine(_fillingCoroutine);

            _fillingCoroutine = StartCoroutine(Inflate(_capacity.FillingRatio, duration));
        }

        private IEnumerator Inflate(float fillingRatio, float duration)
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();
            
            Vector3 startScale = _model.localScale;
            Vector3 targetScale = Vector3.Lerp(_startScale, _endScale, fillingRatio);

            float time = 0;
            while (time < duration)
            {
                _model.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
                
                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }

            _model.localScale = targetScale;
        }
    }
}