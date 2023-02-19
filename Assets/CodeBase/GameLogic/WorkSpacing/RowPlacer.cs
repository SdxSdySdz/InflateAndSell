using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic.WorkSpacing
{
    public class RowPlacer : MonoBehaviour
    {
        [SerializeField] private float _distanceBetween;
        [SerializeField] private float _focusingDuration;

        private List<Transform> _transforms;
        private Coroutine _focusingCoroutine;
        
        public int Count => _transforms.Count;

        private Vector3 Direction => Vector3.right;
        
        private void Awake()
        {
            _transforms = new List<Transform>();
        }

        private void OnValidate()
        {
            if (_transforms != null)
                UpdatePositions();
        }

        public void AddFirst(Transform child)
        {
            _transforms.Insert(0, child);
            transform.position -= transform.TransformDirection(Direction) * _distanceBetween;

            Adopt(child);
            UpdatePositions();
        }

        public void AddLast(Transform child)
        {
            _transforms.Add(child);
            Adopt(child);
            UpdatePositions();
        }

        public Transform Get(int index)
        {
            return _transforms[index];
        }

        public void Focus(int index, Vector3 position)
        {
            Vector3 offset = position - _transforms[index].position;
            if (offset.magnitude < float.Epsilon)
                return;
            
            if (_focusingCoroutine != null)
                StopCoroutine(_focusingCoroutine);


            _focusingCoroutine = StartCoroutine(Move(offset, _focusingDuration));
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < _transforms.Count; i++)
            {
                _transforms[i].localPosition = (i * _distanceBetween) * Direction;
            }
        }

        private void Adopt(Transform child)
        {
            child.SetParent(transform);
        }


        private IEnumerator Move(Vector3 offset, float duration)
        {
            var waitForEndOfFrame = new WaitForEndOfFrame();

            Vector3 startPosition = transform.position;
            Vector3 endPosition = transform.position + offset;

            float time = 0;
            while (time < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);

                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }

            transform.position = endPosition;
        }
    }
}