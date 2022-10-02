using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class MoneyController:MonoBehaviour
    {
        public TextMeshProUGUI MoneyText;
        private int _money;
        private const string MoneyKey = "Money";

        private void Start()
        {
            _money = PlayerPrefs.GetInt(MoneyKey, 0); 
            ChangeText();
        }

        public void ChangeMoney(int money)
        {
            _money += money;
            PlayerPrefs.SetInt(MoneyKey, _money);
            ChangeText();
        }

        private void ChangeText()
        {
            if (MoneyText!=null)
                MoneyText.text = $"{_money}";
        }
    }
}