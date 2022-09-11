using Code.Components.Interfaces;
using Code.Configs;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace Code.UI
{
    public class ItemUIView:MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _useButton;
        private IItem _item;
        private Inventory _inventory;
        public bool CraftMode = false;
        private bool _isSelect = false;

        public void SetNewItemParameters(IItem item, CharacterData characterData, Inventory inventory)
        {
            _image.sprite = item.Sprite;
            _item = item;
            _item.Owner = characterData;
            _inventory = inventory;
            _useButton.onClick.AddListener(ClickButton);
            
        }

        private void ClickButton()
        {
            if (CraftMode)
            {
                if (_item is ICraftable parts)
                {
                    if (_isSelect)
                    {
                        _inventory.CheckCraft(parts, false);
                        _image.color = Color.white;
                        _isSelect = false;
                    }
                    else
                    {
                        parts.CurrentGameObject = gameObject;
                        _inventory.CheckCraft(parts, true);
                        _image.color = Color.red;
                        _isSelect = true;
                    }
                }
            }
            else
            {
                if (_item is ItemsCfg.HealthItem restore)
                {
                    if (restore.Owner.TryGetComponent<ITakeDamage>(out var takeDamage))
                    {
                        takeDamage.Damage(-restore.RestoreHealth);
                        Destroy(gameObject);
                        _inventory.DestroyItem(this);
                    }
                }
            }
        }
    }
}