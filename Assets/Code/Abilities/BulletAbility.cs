using System.Collections.Generic;
using Code.Behaviours;
using Code.Components;
using Code.Components.Character;
using Code.Components.Interfaces;
using Photon.Pun;
using Unity.Entities;
using UnityEngine;

namespace Code.Abilities
{
    public class BulletAbility:MonoBehaviour,ICollisionAbility
    {
        public IPool<GameObject> BulletPool;
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
            {
                ReturnToPool();
                //PhotonNetwork.Destroy(gameObject);
            }
        }

        public void DestroyPoolTag()
        { 
            if (_collisionAbility.Entity == Entity.Null)
                return;
            _collisionAbility.EntityManager.RemoveComponent<InPool>(_collisionAbility.Entity);
            GetComponent<BulletData>().Convert(_collisionAbility.Entity, _collisionAbility.EntityManager, null);
        }

        public void ReturnToPool()
        {
            _collisionAbility.EntityManager.AddComponent<InPool>(_collisionAbility.Entity);
            BulletPool.ReturnToPool(gameObject);
        }

        public void Init(CollisionAbility parent)
        {
            _collisionAbility = parent;
        }
    }
}