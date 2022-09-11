using UnityEngine;

namespace Code.Components.Interfaces
{
    public interface ICraftable
    {
        public string Name { get; }
        public GameObject CurrentGameObject { get; set; }
    }
}