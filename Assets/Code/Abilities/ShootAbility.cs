using System.IO;
using System.Threading.Tasks;
using Code.Components.Interfaces;
using Code.Configs;
using Code.Network;
using Code.Utils;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Code.Abilities
{
    public class ShootAbility:MonoBehaviour, IAbility
    {
        public bool JumpBullet = false;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _shootDelay;
        private IPool<GameObject> _pool;
        private GameConfig _gameConfig;
        private float _lastShootTime = 0;

        //[Inject]
        public async void Awake()
        {
            await LoadConfig();
            _shootDelay = _gameConfig.shootDelay;
            _pool = new BulletPool(_bulletPrefab);
        }
        
        private async Task LoadConfig()
        {
            var stringData = File.ReadAllText(Application.dataPath + "/PlayerConfig.txt");
            _gameConfig = JsonUtility.FromJson<GameConfig>(stringData);
        }
        public void Execute()
        {
            if (Time.time<=_lastShootTime+_shootDelay)
                return;
            _lastShootTime = Time.time;
            if (_bulletPrefab == null)
            {
                Debug.Log("Shoot without prefab");
                return;
            }
            var playerTransform = transform;
            var bulletGO = _pool.GetObject();
            bulletGO.transform.position = _shootPoint.position;
            bulletGO.transform.rotation = playerTransform.rotation;
            if (bulletGO.TryGetComponent<BulletAbility>(out var bullet))
            {
                bullet.DestroyPoolTag();
                bullet.IsJump = JumpBullet;
                bullet.BulletPool = _pool;
            }
        }
    }
}