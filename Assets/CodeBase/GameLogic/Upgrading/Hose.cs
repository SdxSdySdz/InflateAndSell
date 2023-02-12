using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;

namespace CodeBase.GameLogic.Upgrading
{
    public class Hose : MultiplierBasedUpgradeable
    {
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