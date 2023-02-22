using CodeBase.Data;
using CodeBase.GameLogic.Upgrading;
using CodeBase.GameLogic.WorkSpacing;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.GameLogic.Marketing
{
    public class Market : IProgressReader, IProgressWriter, IUpgradeable
    {
        private const int BarrelCost = 50;
        private const float IncreasingMultiplier = 1.15f;

        private int _level;
        
        public int Sell(Barrel barrel)
        {
            return BarrelCost;
        }

        public int GetWorkSpaceCost()
        {
            if (_level == 0)
                return BarrelCost * 3;

            float time;
            if (_level <= 10)
                time = _level * 10;
            else
            {
                float baseTime = 300;
                time = baseTime * Mathf.Pow(IncreasingMultiplier, _level - 10);
            }
            
            float averageDuration = 1.75f;
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