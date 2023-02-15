using System;
using CodeBase.GameLogic.Upgrading;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public abstract class UpgradingButton<TUpgradeable> : MonoBehaviour
        where TUpgradeable : IUpgradeable
    {
        [SerializeField] private Button _button;

        private TUpgradeable _upgradeable;
        
        public void Construct(TUpgradeable upgradeable)
        {
            _upgradeable = upgradeable;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            if (_upgradeable != null)
                _upgradeable.Upgrade();
        }
    }
}