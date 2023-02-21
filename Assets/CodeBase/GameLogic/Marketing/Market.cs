using CodeBase.Data;
using CodeBase.GameLogic.Upgrading;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.Infrastructure.Services.Progress;

namespace CodeBase.GameLogic.Marketing
{
    public class Market : IProgressReader, IProgressWriter, IUpgradeable
    {
        private const int BarrelCost = 50;

        private int _level;
        
        public int Sell(Barrel barrel)
        {
            return BarrelCost;
        }

        public int GetWorkSpaceCost()
        {
            if (_level == 0)
                return BarrelCost * 3;
            
            float averageDuration = 5f;
            int time = 10;
            return (int)(_level * (BarrelCost / averageDuration) * time);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _level = progress.Market.Level;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
             progress.Market.Level = _level;
        }

        public void Upgrade()
        {
            _level++;
        }
    }
}