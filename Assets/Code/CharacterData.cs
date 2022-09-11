using System.Collections.Generic;
using Code.Components.Character;
using Code.UI;
using Unity.Entities;
using UnityEngine;

namespace Code
{
    public class CharacterData:MonoBehaviour
    {
        private Inventory _inventory;
        private Entity _entity;
        private EntityManager _entityManager;

        public void Init(Entity entity)
        {
            _entity = entity;
            _inventory = FindObjectOfType<Inventory>();
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
        
        public void GetItem(string name)
        {
            _inventory.CreateNewItem(name, this);
        }

        public void SetShield(float time)
        {
            _entityManager.AddComponentData(_entity, new ShieldComponent()
            {
                Timer = time
            });
        }
    }
}