using System.Collections.Generic;
using Code.Components.Character;
using Code.UI;
using Unity.Entities;
using UnityEngine;

namespace Code
{
    public class CharacterData:MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        private Entity _entity;
        private EntityManager _entityManager;

        public void Init(Entity entity)
        {
            _entity = entity;
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
        
        public void GetItem(string name)
        {
            if (_inventory==null)
                _inventory = FindObjectOfType<Inventory>(); 
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