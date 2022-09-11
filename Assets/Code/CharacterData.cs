using Code.UI;
using UnityEngine;

namespace Code
{
    public class CharacterData:MonoBehaviour
    {
        private Inventory _inventory;

        private void Awake() => 
            _inventory = FindObjectOfType<Inventory>();

        public void GetItem(string name)
        {
            _inventory.CreateNewItem(name, this);
        }
    }
}