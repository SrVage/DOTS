using Code.Components.Interfaces;
using Code.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ItemUIView:MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _useButton;
        public IItem Item;
        private Inventory _inventory;
        public bool CraftMode = false;
        private bool _isSelect = false;

        public void SetNewItemParameters(IItem item, CharacterData characterData, Inventory inventory)
        {
            _image.sprite = item.Sprite;
            Item = item;
            Item.Owner = characterData;
            _inventory = inventory;
            _useButton.onClick.AddListener(ClickButton);
            
        }

        public void Destroy()
        {
            _inventory.DestroyItem(this);
            Destroy(gameObject);
        }

        private void ClickButton()
        {
            if (CraftMode)
            {
                if (Item is ICraftable parts)
                {
                    if (_isSelect)
                    {
                        _inventory.CheckCraft(parts.Name, false);
                        _image.color = Color.white;
                        _isSelect = false;
                    }
                    else
                    {
                        parts.CurrentItem = this;
                        _inventory.CheckCraft(parts.Name, true);
                        _image.color = Color.red;
                        _isSelect = true;
                    }
                }
            }
            else
            {
                if (Item is ItemsCfg.HealthItem restore)
                {
                    if (restore.Owner.TryGetComponent<ITakeDamage>(out var takeDamage))
                    {
                        takeDamage.Damage(-restore.RestoreHealth);
                        Destroy(gameObject);
                        _inventory.DestroyItem(this);
                    }
                }
                if (Item is ItemsCfg.ArmourItem armour)
                {
                   
                    armour.Owner.SetShield(armour.Armour);
                    Destroy(gameObject);
                    _inventory.DestroyItem(this);
                }
            }
        }
    }
}