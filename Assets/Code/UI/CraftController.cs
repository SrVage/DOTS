using System.Collections.Generic;
using System.Linq;
using Code.Components.Interfaces;
using Code.Configs;
using UnityEngine;

namespace Code.UI
{
    public class CraftController
    {
        private readonly List<ICraftable> _craftableItems;
        private readonly CraftsCfg _craftsCfg;
        private readonly Inventory _inventory;

        public CraftController(CraftsCfg craftsCfg, Inventory inventory)
        {
            _craftableItems = new List<ICraftable>();
            _craftsCfg = craftsCfg;
            _inventory = inventory;
        }

        public void AddItem(ICraftable craftItem)
        {
            _craftableItems.Add(craftItem);
            Check();
        }

        public void DeleteItem(ICraftable craftItem)
        {
            if (_craftableItems.Contains(craftItem))
                _craftableItems.Remove(craftItem);
        }

        private void Check()
        {
            if (_craftableItems.Count<2)
                return;
            foreach (var receipt in _craftsCfg.Receipts)
            {
                if (receipt.ItemsNames.Length != _craftableItems.Count)
                    continue;
                var count = _craftableItems.Count;
                foreach (var item in _craftableItems)
                {
                    if (receipt.ItemsNames.Contains(item.Name))
                        count--;
                }
                if (count == 0)
                {
                    Finish(receipt.NewItem);
                    break;
                }
            }
        }

        private void Finish(string newItem)
        {
            foreach (var item in _craftableItems) 
                GameObject.Destroy(item.CurrentGameObject);
            _inventory.CreateNewItem(newItem);
        }
    }
}