using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HoseProgress HoseProgress;
        public LeverProgress LeverProgress;

        public PlayerProgress()
        {
            HoseProgress = new HoseProgress();
            LeverProgress = new LeverProgress();
        }
    }
}