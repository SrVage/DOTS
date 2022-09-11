using System;
using System.Collections.Generic;
using System.Linq;
using Code.Components.Interfaces;
using Code.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.UI
{
    public class CraftController
    {
        private readonly List<string> _craftableItems;
        private readonly CraftsCfg _craftsCfg;
        private readonly Inventory _inventory;

        public CraftController(CraftsCfg craftsCfg, Inventory inventory)
        {
            _craftableItems = new List<string>();
            _craftsCfg = craftsCfg;
            foreach (var receipt in _craftsCfg.Receipts)
            {
                Array.Sort(receipt.ItemsNames, StringComparer.InvariantCulture);
            }
            _inventory = inventory;
        }

        public void AddItem(string craftItem)
        {
            _craftableItems.Add(craftItem);
            Check();
        }

        public void DeleteItem(string craftItem)
        {
            if (_craftableItems.Contains(craftItem))
                _craftableItems.Remove(craftItem);
        }

        private void Check()
        {
            if (_craftableItems.Count<2)
                return;
            var craftablesNames = _craftableItems.ToArray();
            Array.Sort(craftablesNames, StringComparer.InvariantCulture);
            foreach (var receipt in _craftsCfg.Receipts)
            {
                if (receipt.ItemsNames.SequenceEqual(craftablesNames)) 
                    Finish(receipt.NewItem);
            }
        }

        private void Finish(string newItem)
        {
            foreach (var t in _craftableItems)
            {
                _inventory.DestroyItem(t);
            }
            _craftableItems.Clear();
            _inventory.CreateNewItem(newItem);
        }
    }
}