using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class BuyWorkPlaceWindow : MonoBehaviour
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _model;
        
        public event UnityAction BuyButtonClicked;
        
        private void Awake()
        {
            Hide();
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnButtonClicked);
        }

        public void Show()
        {
            _model.SetActive(true);
        }

        public void Hide()
        {
            _model.SetActive(false);
        }

        private void OnButtonClicked()
        {
            BuyButtonClicked?.Invoke();
        }
    }
}