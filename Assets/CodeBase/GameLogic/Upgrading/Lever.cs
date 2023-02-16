using CodeBase.Data;

namespace CodeBase.GameLogic.Upgrading
{
    public class Lever : MultiplierBasedUpgradeable
    {
        public float PushDuration => CurrentValue;

        protected override float BaseValue => 0.5f;
        protected override float Multiplier => 0.89f;

        protected override int ReadLevel(PlayerProgress progress)
        {
            return progress.Lever.Level;
        }

        protected override void WriteLevel(PlayerProgress progress, int level)
        {
            progress.Lever.Level = level;
        }
    }
}