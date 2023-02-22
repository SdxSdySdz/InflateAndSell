using UnityEngine;

namespace CodeBase
{
    public class MainProvider : MonoBehaviour
    {
        [SerializeField] private Main _mainPrefab;
        [SerializeField] private bool _isProgressClearingNeeded;

        private void Awake()
        {
            if (_isProgressClearingNeeded)
                PlayerPrefs.DeleteAll();

            var main = FindObjectOfType<Main>();
            if (main == null)
                Instantiate(_mainPrefab);
            
            Destroy(gameObject);
        }
    }
}