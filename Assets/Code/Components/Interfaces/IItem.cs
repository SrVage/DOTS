using Unity.Entities;
using UnityEngine;

namespace Code.Components.Interfaces
{
    public interface IItem
    {
        public string Name { get; }
        public CharacterData Owner { get; set; }
        public Sprite Sprite { get; }
    }
}