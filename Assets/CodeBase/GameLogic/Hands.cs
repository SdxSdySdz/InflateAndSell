using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic
{
    public class Hands : MonoBehaviour
    {
        [SerializeField] private float _pickingDuration;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private ParticleSystem _moneyParticles;
        
        private Coroutine _pickingCoroutine;

        private void Awake()
        {
            _moneyParticles.Stop();
        }

        public void PickUp(Barrel barrel, Action onStart = null, Action onFinish = null)
        {
            if (_pickingCoroutine != null)
                StopCoroutine(_pickingCoroutine);

            _pickingCoroutine = StartCoroutine(PickUpWithHands(barrel, onStart, onFinish));
        }

        private IEnumerator PickUpWithHands(Barrel barrel, Action onStart = null, Action onFinish = null)
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();
            
            onStart?.Invoke();

            List<IEnumerator> pickings = new()
            {
                PickUpWithHand(_leftHand, barrel.LeftHandlePosition, _pickingDuration),
                PickUpWithHand(_rightHand, barrel.RightHandlePosition, _pickingDuration, barrel),
            };
            
            bool isNotFinished;
            do
            {
                isNotFinished = false;
                foreach (var picking in pickings)
                {
                    isNotFinished = picking.MoveNext() || isNotFinished;
                }

                yield return waitForEndOfFrame;
            } while (isNotFinished);

            ThrowMoney();
            onFinish?.Invoke();
        }

        private IEnumerator PickUpWithHand(Transform hand, Vector3 handlePosition, float duration, Barrel barrel = null)
        {
            Vector3 startPosition = hand.position;
            
            foreach (var _ in Move(hand, startPosition, handlePosition, duration / 2f))
            {
                yield return _;
            }

            bool isDragNeeded = barrel != null;
            if (isDragNeeded)
                barrel.transform.SetParent(hand);
            
            foreach (var _ in Move(hand, handlePosition, startPosition, duration / 2f))
            {
                yield return _;
            }
        }

        private IEnumerable Move(Transform hand, Vector3 startPosition, Vector3 targetPosition, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                hand.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                
                time += Time.deltaTime;
                yield return null;
            }

            hand.position = targetPosition;
        }

        private void ThrowMoney()
        {
            _moneyParticles.Play();
        }
    }
}