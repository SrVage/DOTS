using System;
using System.Collections.Generic;
using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/Items Config")]
    public class ItemsCfg:ScriptableObject
    {
        public List<IItem> Items;
        public List<HealthItem> HealthItems;
        public List<PartsOfItem> PartsOfItems;

        public void Init()
        {
            Items = new List<IItem>();
            Items.AddRange(HealthItems);
            Items.AddRange(PartsOfItems);
        }

        [Serializable]
        public abstract class Item : IItem
        {
            [SerializeField] private string _name;
            [SerializeField] private Sprite _sprite;
            public string Name => _name;
            public CharacterData Owner { get; set; }
            public Sprite Sprite => _sprite;
        }
        
        [Serializable]
        public class HealthItem : Item
        {
            public float _restoreHealth;
            public float RestoreHealth => _restoreHealth;
        }
        
        [Serializable]
        public class PartsOfItem : Item, ICraftable
        {
            public GameObject CurrentGameObject { get; set; }
        }
    }
}