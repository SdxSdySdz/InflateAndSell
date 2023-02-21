using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HoseProgress Hose;
        public LeverProgress Lever;
        public WalletProgress Wallet;
        public CompanyProgress Company;
        public MarketProgress Market;

        public PlayerProgress()
        {
            Hose = new HoseProgress();
            Lever = new LeverProgress();
            Wallet = new WalletProgress();
            Company = new CompanyProgress();
            Market = new MarketProgress();
        }
    }
}