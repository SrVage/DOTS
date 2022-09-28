using System;
using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Abilities
{
    public class SoundAbility:MonoBehaviour, ISoundAbility
    {
        [SerializeField] private AudioClip _clip;
        private EffectSource _effectSource;

        private void Awake()
        {
            _effectSource = FindObjectOfType<EffectSource>();
        }

        public void Execute()
        {
            _effectSource.Play(_clip, transform.position);
        }
    }
}