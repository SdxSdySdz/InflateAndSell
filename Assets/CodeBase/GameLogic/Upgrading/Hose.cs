using CodeBase.Data;

namespace CodeBase.GameLogic.Upgrading
{
    public class Hose : MultiplierBasedUpgradeable
    {
        public float PassingVolume => CurrentValue;

        protected override float BaseValue => 3f;
        protected override float Multiplier => 1.11f;

        protected override int ReadLevel(PlayerProgress progress)
        {
            return progress.Hose.Level;
        }

        protected override void WriteLevel(PlayerProgress progress, int level)
        {
            progress.Hose.Level = level;
        }
    }
}