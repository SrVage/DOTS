using Code.Components.Interfaces;
using UnityEngine;

namespace Code
{
    public class EffectSource:MonoBehaviour, IChangeSoundVolume
    {
        private AudioSource _audioSource;

        private void Awake() => 
            _audioSource = GetComponent<AudioSource>();

        public void Play(AudioClip clip, Vector3 position)
        {
            transform.position = position;
            _audioSource.PlayOneShot(clip);
        }
        
        public void Change(float volume) => 
            _audioSource.volume = volume;
    }
}