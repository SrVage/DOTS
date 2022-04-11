using System;
using Code.Components.Character;
using UnityEngine;

namespace Code.Behaviours
{
    public class FollowBehaviour:Behaviour
    {
        [SerializeField] private Transform _character;
        [SerializeField] private float _speed = 1f;

        private void Start()
        {
            _character = FindObjectOfType<UserInputData>().transform;
        }

        public override float Evaluate()
        {
            if (_character == null) return float.MaxValue;
            return (_character.position - transform.position).sqrMagnitude;
        }

        public override void Behave()
        {
            transform.LookAt(_character);
            transform.Translate(Vector3.forward*_speed*Time.deltaTime);
        }
    }
}