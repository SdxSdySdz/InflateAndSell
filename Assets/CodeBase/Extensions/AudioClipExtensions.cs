using System;
using UnityEngine;

namespace CodeBase.Extensions
{
    public static class AudioClipExtensions
    {
        private const string SubClipNameAddition = "-sub";

        public static AudioClip SubClip01(this AudioClip clip, float start, float end = 1f)
        {
            float duration = clip.length;
            return SubClip(clip, start * duration, end * duration);
        }
        
        public static AudioClip SubClip(this AudioClip clip, float startSecond, float endSecond = float.MaxValue)
        {
            if (startSecond > endSecond)
                throw new ArgumentOutOfRangeException(nameof(startSecond));
            
            endSecond = Mathf.Min(endSecond, clip.length);

            int frequency = clip.frequency;
            float timeLength = endSecond - startSecond;
            int samplesLength = (int)(frequency * timeLength);
            
            AudioClip subClip = AudioClip.Create(
                clip.name + SubClipNameAddition, 
                samplesLength, 
                1, 
                frequency, 
                false);
            
            var data = new float[samplesLength];
            clip.GetData(data, (int)(frequency * startSecond));
            
            subClip.SetData(data, 0);
            
            return subClip;
        }
    }
}