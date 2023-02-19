using System;
using TMPro;
using UnityEngine;

namespace CodeBase.GameLogic.Player
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private int _amount;
        
        public int Amount
        {
            get => _amount;
            
            private set
            {
                _amount = value;
                UpdateView();
            }
        }

        public void Construct(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Amount = amount;
        }

        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Amount += amount;
        }

        private void UpdateView()
        {
            _text.text = $"{_amount} $";
        }
    }
}