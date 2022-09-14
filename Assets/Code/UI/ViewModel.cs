using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

namespace Code.UI
{
    [Binding]
    public class ViewModel:MonoBehaviour, INotifyPropertyChanged
    {
        private string _health;
        private string _enemyHealth;
        private string _time;
        public event PropertyChangedEventHandler PropertyChanged;
        [Binding]
        public string Health
        {
            get => _health;
            set
            {
                _health = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Health)));
            }
        }
        [Binding]
        public string EnemyHealth
        {
            get => _enemyHealth;
            set
            {
                _enemyHealth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnemyHealth)));
            }
        }
        
        [Binding]
        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            }
        }
    }
}