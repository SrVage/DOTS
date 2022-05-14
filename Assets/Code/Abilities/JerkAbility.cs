using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Code.Components.Interfaces;
using Code.Configs;
using UnityEngine;
using Zenject;

namespace Code.Abilities
{
    public class JerkAbility:MonoBehaviour, IAbility
    {
        private const float Distance = 0.1f;
        [SerializeField] private float _distance;
        [SerializeField] private float _speed;
        [SerializeField] private float _rechargeTime;
        private Vector3 _endPoint;
        private float _time;
        private GameConfig _gameConfig;
        
        public async void Awake()
        {
            await LoadConfig();
            _distance = _gameConfig.jerkDistance;
            _speed = _gameConfig.jerkSpeed;
            _rechargeTime = _gameConfig.jerkRechargeTime;
        }
        
        private async Task LoadConfig()
        {
            var stringData = File.ReadAllText(Application.dataPath + "/PlayerConfig.txt");
            _gameConfig = JsonUtility.FromJson<GameConfig>(stringData);
        }
        public void Execute()
        {
            if (_time > Time.time)
                return;
            _time = Time.time + _rechargeTime;
            _endPoint = transform.position + transform.forward * _distance;
            StartCoroutine(nameof(Jerk));
        }

        private IEnumerator Jerk()
        {
            while (Vector3.SqrMagnitude(_endPoint - transform.position)>Distance)
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
                yield return null;
            }
        }
    }
}