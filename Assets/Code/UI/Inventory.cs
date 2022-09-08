using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class Inventory:MonoBehaviour
    {
        [SerializeField] private Button _openInventory;
        [SerializeField] private Button _closeInventory;
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private Transform _itemHolder;

        private void Awake()
        {
            _openInventory.onClick.AddListener(OpenInventory);
            _closeInventory.onClick.AddListener(CloseInventory);
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
    }
}