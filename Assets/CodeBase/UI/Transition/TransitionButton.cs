using System;
using CodeBase.GameLogic.WorkSpacing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Transition
{
    [RequireComponent(typeof(Button))]
    public abstract class TransitionButton : MonoBehaviour
    {
        private Button _button;
        private BuyWorkPlaceWindow _buyWorkPlaceWindow;
        
        protected Company Company { get; private set; }
        protected abstract bool IsNeedToBuy { get; }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Construct(Company company, BuyWorkPlaceWindow buyWorkPlaceWindow)
        {
            Company = company;
            _buyWorkPlaceWindow = buyWorkPlaceWindow;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(TryToOtherWorkSpace);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(TryToOtherWorkSpace);
        }

        protected abstract void ToOtherWorkSpace();
        
        private void TryToOtherWorkSpace()
        {
            if (IsNeedToBuy)
            {
                _buyWorkPlaceWindow.Show();
                return;
            }
            
            ToOtherWorkSpace();
        }
    }
}