using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public abstract class MultiplierBasedUpgradeable : MonoBehaviour, IUpgradeable, IProgressReader, IProgressWriter
    {
        private float _currentValue;
        private int _level;
        
        protected abstract float BaseValue { get; }
        protected abstract float Multiplier { get; }
        
        public void Upgrade()
        {
            _currentValue = GetNextValue(_currentValue);
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _level = ReadLevel(progress);
        }
    
        public void UpdateProgress(PlayerProgress progress)
        {
            WriteLevel(progress, _level);
        }
        
        protected abstract int ReadLevel(PlayerProgress progress);
        protected abstract void WriteLevel(PlayerProgress progress, int level);
        
        private float GetNextValue(float currentValue)
        {
            return currentValue * Mathf.Pow(Multiplier, _level);
        }
    }
}