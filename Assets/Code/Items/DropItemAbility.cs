using System.Collections.Generic;
using Code.Abilities;
using Code.Components.Interfaces;
using Unity.Entities;
using UnityEngine;

namespace Code.Items
{
    public class DropItemAbility:MonoBehaviour, ICollisionAbility, IConvertGameObjectToEntity
    {
        [SerializeField] protected string _name;
        public string Name => _name;
        protected List<Collider> _collisions => _collisionAbility.Colliders;
        
        private CollisionAbility _collisionAbility;
        private Entity _entity;
        private EntityManager _dstManager;

        public virtual void Execute()
        {
            foreach (var collision in _collisions)
            {
                if (collision.TryGetComponent<CharacterData>(out var character))
                {
                    character.GetItem(Name);
                    _dstManager.DestroyEntity(_entity);
                    Destroy(gameObject);
                }
            }
        }

        public void Init(CollisionAbility parent)
        {
            _collisionAbility = parent;
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            _entity = entity;
            _dstManager = dstManager;
        }
    }
}