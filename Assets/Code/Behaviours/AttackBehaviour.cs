using Code.Abilities;
using Code.Components.Character;
using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Behaviours
{
    public class AttackBehaviour:Behaviour
    {
        [SerializeField] private Transform _character;
        private ITakeDamage _takeDamage;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _attackDistance = 5f;
        
        private void Start()
        {
            _character = FindObjectOfType<UserInputData>().transform;
            _takeDamage = _character.GetComponent<ITakeDamage>();
        }

        public override float Evaluate()
        {
            var distance = (_character.position - transform.position).sqrMagnitude;
            return distance<_attackDistance?1:float.MaxValue;
        }

        public override void Behave()
        {
            _takeDamage.Damage(_damage);
        }
    }
}