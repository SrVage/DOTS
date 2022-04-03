using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Abilities
{
    public class ShootAbility:MonoBehaviour, IAbility
    {
        public bool JumpBullet = false;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _shootDelay;
        private float _lastShootTime = 0;
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
            var bulletGO = Instantiate(_bulletPrefab, _shootPoint.position, playerTransform.rotation);
            if (bulletGO.TryGetComponent<BulletAbility>(out var bullet))
            {
                bullet.IsJump = JumpBullet;
            }
        }
    }
}