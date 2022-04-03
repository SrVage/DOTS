using System.Collections.Generic;
using Code.Components;
using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Abilities
{
    public class BulletAbility:MonoBehaviour,ICollisionAbility
    {
        public bool IsJump = false;
        protected List<Collider> _collisions => _collisionAbility.Colliders;
        private CollisionAbility _collisionAbility;
        public void Execute()
        {
            if (IsJump)
                _collisionAbility.EntityManager.AddComponentData(_collisionAbility.Entity, new Rotate());
            else 
                GameObject.Destroy(gameObject);
        }

        public void Init(CollisionAbility parent)
        {
            _collisionAbility = parent;
        }
    }
}