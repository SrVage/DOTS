using System.Collections.Generic;
using Code.Components.Interfaces;
using Photon.Pun;
using Unity.Entities;
using UnityEngine;

namespace Code.Abilities
{
    public class GetBulletAbility:MonoBehaviour, ICollisionAbility
    {
        private CollisionAbility _collisionAbility;
        protected List<Collider> _collisions => _collisionAbility.Colliders;

        public void Execute()
        {
            foreach (var collision in _collisions)
            {
                if (collision.TryGetComponent<IChangeBullet>(out var change))
                {
                   change.GetBullet();
                   PhotonNetwork.Destroy(gameObject);
                }
            }    
        }

        public void Init(CollisionAbility parent)
        {
            _collisionAbility = parent;
        }
    }
}