using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.GameLogic.WorkSpacing;
using UnityEngine;

namespace CodeBase.GameLogic.Marketing
{
    public class Hands : MonoBehaviour
    {
        [SerializeField] private float _pickingDuration;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private MoneyEffect _moneyEffect;

        [SerializeField] private Transform _leftHandOrigin;
        [SerializeField] private Transform _rightHandOrigin;
        
        private Coroutine _pickingCoroutine;

        private void Awake()
        {
            _moneyEffect.Stop();
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
                PickUpWithHand(
                    barrel, 
                    _leftHand, 
                    _leftHandOrigin,
                    barrel.LeftHandlePosition, 
                    _pickingDuration
                    ),
                
                PickUpWithHand(
                    barrel, 
                    _rightHand, 
                    _rightHandOrigin,
                    barrel.RightHandlePosition, 
                    _pickingDuration, 
                    true
                    ),
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

        private IEnumerator PickUpWithHand(
            Barrel barrel, 
            Transform hand, 
            Transform origin,
            Transform handle,
            float duration, 
            bool isDragNeeded = false
            )
        {
            foreach (var _ in Move(hand, origin.position, handle, duration / 2f))
            {
                yield return _;
            }

            if (isDragNeeded)
                barrel.transform.SetParent(hand);
            
            foreach (var _ in Move(hand, handle.position, origin, duration / 2f))
            {
                yield return _;
            }
        }

        private IEnumerable Move(Transform hand, Transform startPosition, Transform targetPosition, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                hand.position = Vector3.Lerp(startPosition.position, targetPosition.position, time / duration);
                
                time += Time.deltaTime;
                yield return null;
            }

            hand.position = targetPosition.position;
        }
        
        private IEnumerable Move(Transform hand, Vector3 startPosition, Transform targetPosition, float duration)
        {
            float time = 0;
            while (time < duration)
            {
                hand.position = Vector3.Lerp(startPosition, targetPosition.position, time / duration);
                
                time += Time.deltaTime;
                yield return null;
            }

            hand.position = targetPosition.position;
        }

        private void ThrowMoney()
        {
            _moneyEffect.Play();
        }
    }
}