using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public abstract class MultiplierBasedUpgradeable : MonoBehaviour, IUpgradeable, IProgressReader, IProgressWriter
    {
        private float _baseValue;
        private int _level;
        
        public void Upgrade()
        {
            _baseValue = GetNextValue(_baseValue);
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            _level = ReadLevel(progress);
        }
    
        public void UpdateProgress(PlayerProgress progress)
        {
            WriteLevel(progress, _level);
        }

        protected abstract float GetNextValue(float currentValue);
        protected abstract int ReadLevel(PlayerProgress progress);
        protected abstract void WriteLevel(PlayerProgress progress, int level);
    }
}