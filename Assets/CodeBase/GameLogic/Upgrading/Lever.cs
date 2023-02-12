using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;

namespace CodeBase.GameLogic.Upgrading
{
    public class Lever : MultiplierBasedUpgradeable
    {
        protected override float BaseValue => 1f;
        protected override float Multiplier => 1.07f;

        protected override int ReadLevel(PlayerProgress progress)
        {
            return progress.LeverProgress.Level;
        }

        protected override void WriteLevel(PlayerProgress progress, int level)
        {
            progress.LeverProgress.Level = level;
        }
    }
}