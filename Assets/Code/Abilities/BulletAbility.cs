using System.Collections.Generic;
using Code.Behaviours;
using Code.Components;
using Code.Components.Interfaces;
using Photon.Pun;
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
            foreach (var collision in _collisions)
            {
                if (collision.TryGetComponent<FreezeBehaviour>(out var freeze))
                {
                    freeze.IsFreeze = true;
                }
            }
            if (IsJump)
                _collisionAbility.EntityManager.AddComponentData(_collisionAbility.Entity, new Rotate());
            else 
                PhotonNetwork.Destroy(gameObject);
        }

        public void Init(CollisionAbility parent)
        {
            _collisionAbility = parent;
        }
    }
}