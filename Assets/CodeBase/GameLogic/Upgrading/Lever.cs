using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;

namespace CodeBase.GameLogic.Upgrading
{
    public class Lever : MultiplierBasedUpgradeable
    {
        protected override float GetNextValue(float currentValue)
        {
            throw new System.NotImplementedException();
        }

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