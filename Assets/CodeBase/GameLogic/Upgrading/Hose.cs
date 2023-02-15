using CodeBase.Data;

namespace CodeBase.GameLogic.Upgrading
{
    public class Hose : MultiplierBasedUpgradeable
    {
        public float PassingVolume => CurrentValue;

        
        protected override float BaseValue => 1f;
        protected override float Multiplier => 1.11f;

        protected override int ReadLevel(PlayerProgress progress)
        {
            return progress.HoseProgress.Level;
        }

        protected override void WriteLevel(PlayerProgress progress, int level)
        {
            progress.HoseProgress.Level = level;
        }
    }
}