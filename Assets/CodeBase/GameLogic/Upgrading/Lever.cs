using CodeBase.Data;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.GameLogic.Upgrading
{
    public class Lever : MultiplierBasedUpgradeable
    {
        public float PushDuration => CurrentValue;

        protected override float BaseValue => 1f;
        protected override float Multiplier => 0.89f;

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