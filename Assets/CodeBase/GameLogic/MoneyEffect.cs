using UnityEngine;

namespace CodeBase.GameLogic
{
    public class MoneyEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private AudioSource _audio;

        public void Play()
        {
            _particles.Play();
            _audio.Play();
        }

        public void Stop()
        {
            _particles.Stop();
            _audio.Stop();
        }
    }
}