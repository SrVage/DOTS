using UnityEngine;

namespace Code.UI
{
    public class PurchasingController:MonoBehaviour
    {
        [SerializeField] private MoneyController _moneyController;
        
        public void BuyMoneyFirst() => 
            _moneyController.ChangeMoney(200);
    }
}