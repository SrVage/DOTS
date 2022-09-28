using Code.Components.Interfaces;
using UnityEngine;

namespace Code
{
    public class MusicSource:MonoBehaviour, IChangeSoundVolume
    {
        private AudioSource _audioSource;

        private void Awake() => 
            _audioSource = GetComponent<AudioSource>();


        public void Change(float volume) => 
            _audioSource.volume = volume;
    }
}