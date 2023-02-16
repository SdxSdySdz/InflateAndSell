﻿using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HoseProgress Hose;
        public LeverProgress Lever;
        public WalletProgress Wallet;

        public PlayerProgress()
        {
            Hose = new HoseProgress();
            Lever = new LeverProgress();
            Wallet = new WalletProgress();
        }
    }
}