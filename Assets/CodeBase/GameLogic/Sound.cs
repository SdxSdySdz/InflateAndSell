using System;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.GameLogic
{
    [RequireComponent(typeof(AudioSource))]
    public class Sound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        [Header("Cropping")] 
        
        [Range(0, 1)] 
        [SerializeField] private float _start = 0f;
        [Range(0, 1)] 
        [SerializeField] private float _end = 1f;
        
        private AudioSource _source;

        private void Awake()
        {
            if (_clip == null)
                throw new NullReferenceException(nameof(_clip));
            
            _source = GetComponent<AudioSource>();
            UpdateClip();
        }

        private void OnValidate()
        {
            _end = Mathf.Max(_start, _end);

            UpdateClip();
        }

        public void Play()
        {
            _source.Play();
        }

        private void UpdateClip()
        {
            _source.clip = _clip.SubClip01(_start, _end);
        }
    }
}