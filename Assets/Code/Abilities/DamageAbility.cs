using System.Collections.Generic;
using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Abilities
{
    public class DamageAbility:MonoBehaviour, ICollisionAbility
    {
        [SerializeField] protected float _damage;
        protected List<Collider> _collisions => _collisionAbility.Colliders;
        private CollisionAbility _collisionAbility;

        public virtual void Execute()
        {
            foreach (var collision in _collisions)
            {
                if (collision.TryGetComponent<ITakeDamage>(out var damage))
                {
                    damage.Damage(_damage);
                }
            }
        }

        public void Init(CollisionAbility parent)
        {
            _collisionAbility = parent;
        }
    }
}