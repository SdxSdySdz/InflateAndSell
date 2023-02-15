using System;

namespace CodeBase.GameLogic
{
    public class Capacity
    {
        private readonly float MaxVolume;
        
        private float _currentVolume;
        
        public Capacity(float maxVolume)
        {
            MaxVolume = maxVolume;
            IsOverflowed = false;
        }
        
        public bool IsOverflowed { get; private set; }
        public float FillingRatio => _currentVolume / MaxVolume;
        
        public void Fill(float volume)
        {
            if (volume < 0)
                throw new ArgumentOutOfRangeException(nameof(volume));
            
            _currentVolume += volume;

            if (_currentVolume >= MaxVolume)
            {
                _currentVolume = MaxVolume;
                IsOverflowed = true;
            }
        }
    }
}