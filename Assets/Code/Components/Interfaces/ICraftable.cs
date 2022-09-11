using Code.UI;
using UnityEngine;

namespace Code.Components.Interfaces
{
    public interface ICraftable
    {
        public string Name { get; }
        public ItemUIView CurrentItem { get; set; }
    }
}