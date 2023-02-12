using System;
using UnityEngine;

namespace CodeBase
{
    public class MainProvider : MonoBehaviour
    {
        [SerializeField] private Main _mainPrefab;

        private void Awake()
        {
            var main = FindObjectOfType<Main>();
            if (main == null)
                Instantiate(_mainPrefab);
            
            Destroy(gameObject);
        }
    }
}