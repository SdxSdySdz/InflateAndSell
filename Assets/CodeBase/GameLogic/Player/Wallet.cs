﻿using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;

namespace CodeBase.GameLogic.Player
{
    public class Wallet : IProgressReader, IProgressWriter
    {
        private int _amount;
        
        public Wallet(int amount = 0)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Amount = amount;
        }

        public int Amount
        {
            get => _amount;
            
            set
            {
                _amount = value;
                Changed?.Invoke(_amount);
            }
        }

        public event Action<int> Changed;

        public void LoadProgress(PlayerProgress progress)
        {
            Amount = progress.Wallet.Amount;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.Wallet.Amount = Amount;
        }

        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Amount += amount;
        }

        public void Spent(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Amount -= amount;
        }
    }
}