using System;
using System.Collections.Generic;
using Code.Components.Interfaces;
using Code.UI;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/Items Config")]
    public class ItemsCfg:ScriptableObject
    {
        public List<IItem> Items;
        public List<HealthItem> HealthItems;
        public List<PartsOfItem> PartsOfItems;
        public List<ArmourItem> ArmourItems;

        public void Init()
        {
            Items = new List<IItem>();
            Items.AddRange(HealthItems);
            Items.AddRange(PartsOfItems);
            Items.AddRange(ArmourItems);
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
        public class ArmourItem:Item
        {
            public float _armour;
            public float Armour => _armour;
        }
        
        [Serializable]
        public class PartsOfItem : Item, ICraftable
        {
            public ItemUIView CurrentItem { get; set; }
        }
    }
}