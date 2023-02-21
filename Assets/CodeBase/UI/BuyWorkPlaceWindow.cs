using System;
using CodeBase.GameLogic.Marketing;
using CodeBase.GameLogic.WorkSpacing;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class BuyWorkPlaceWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _model;

        private Company _company;
        private Market _market;
        
        private void Awake()
        {
            Hide();
        }

        public void Construct(Company company, Market market)
        {
            _company= company;
            _market = market;
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _closeButton.onClick.RemoveListener(Hide);
        }

        public void Show()
        {
            _text.text = $"{_market.GetWorkSpaceCost()} $";
            _buyButton.interactable = _company.CanBuyWorkSpace;
            _model.SetActive(true);
        }

        public void Hide()
        {
            _model.SetActive(false);
        }
        
        private void OnBuyButtonClicked()
        {
            Hide();

            if (_company.IsCurrentWorkSpaceLast)
            {
                _company.SpentMoney(_market.GetWorkSpaceCost());
                _company.ToNextWorkSpace();
            }
            else if (_company.IsCurrentWorkSpaceFirst)
            {
                _company.SpentMoney(_market.GetWorkSpaceCost());
                _company.ToPreviousWorkSpace();
            }
            else
                throw new InvalidOperationException("Trying to buy WorkSpace, " +
                                                    "not being on the edges of company");
        }
    }
}