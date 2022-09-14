using System.Collections.Generic;
using System.Linq;
using Code.Components.Interfaces;
using Code.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class Inventory:MonoBehaviour
    {
        [SerializeField] private Button _openInventory;
        [SerializeField] private Button _closeInventory;
        [SerializeField] private Button _craftButton;
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private Transform _itemHolder;
        [SerializeField] private ItemsCfg _itemsCfg;
        [SerializeField] private ItemUIView _itemUIView;
        [SerializeField] private CraftsCfg _craftsCfg;
        private List<ItemUIView> _listOfInventoryItems= new List<ItemUIView>();
        private TextMeshProUGUI _craftButtonText;
        private CraftController _craftController;
        [SerializeField] private CharacterData _characterData;
        private bool _isCraft = false;

        private void Awake()
        {
            _openInventory.onClick.AddListener(OpenInventory);
            _closeInventory.onClick.AddListener(CloseInventory);
            _craftButton.onClick.AddListener(ChangeCraftMode);
            _craftButtonText = _craftButton.GetComponentInChildren<TextMeshProUGUI>();
            _itemsCfg.Init();
            _craftController = new CraftController(_craftsCfg, this);
        }

        public void CheckCraft(string craftItem, bool add)
        {
            if (add)
                _craftController.AddItem(craftItem);
            else
                _craftController.DeleteItem(craftItem);
        }

        private void ChangeCraftMode()
        {
            if (_isCraft)
            {
                _craftButtonText.text = "Start craft";
                foreach (var item in _listOfInventoryItems)
                {
                    item.CraftMode = false;
                }
                _isCraft = false;
            }
            else
            {
                _craftButtonText.text = "End craft";
                foreach (var item in _listOfInventoryItems)
                { 
                    item.CraftMode = true;
                }
                _isCraft = true;
            }
        }

        private void OpenInventory()
        {
            _inventoryPanel.SetActive(true);
            _openInventory.gameObject.SetActive(false);
        }

        private void CloseInventory()
        {
            _inventoryPanel.SetActive(false);
            _openInventory.gameObject.SetActive(true);
        }

        public void CreateNewItem(string name, CharacterData characterData=null)
        {
            if (characterData != null)
                _characterData = characterData;
            var item = _itemsCfg.Items.FirstOrDefault(i => i.Name == name);
            if (item != null)
            {
                var itemView = Instantiate(_itemUIView, _itemHolder);
                itemView.SetNewItemParameters(item, _characterData, this);
                _listOfInventoryItems.Add(itemView);
                if (_isCraft) 
                    itemView.CraftMode = true;
            }
        }

        public void DestroyItem(ItemUIView item)
        {
            if (_listOfInventoryItems.Contains(item))
                _listOfInventoryItems.Remove(item);
        }
        public void DestroyItem(string item)
        {
            if (_listOfInventoryItems.FirstOrDefault(i => i.Item.Name == item) != null)
                _listOfInventoryItems.First(i => i.Item.Name == item).Destroy();
        }
    }
}