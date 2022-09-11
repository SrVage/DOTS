using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

namespace Code.UI
{
    [Binding]
    public class ViewModel:MonoBehaviour, INotifyPropertyChanged
    {
        private string _health;
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
    }
}