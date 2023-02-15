using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public abstract class MultiplierBasedUpgradeable : MonoBehaviour, IUpgradeable, IProgressReader, IProgressWriter
    {
        private int _level;
        
        protected abstract float BaseValue { get; }
        protected abstract float Multiplier { get; }
        protected float CurrentValue => BaseValue * Mathf.Pow(Multiplier, _level);

        public void Upgrade()
        {
            _level++;
            Debug.LogError($"{GetType().Name} current value {CurrentValue}");
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
    }
}