using Code.Components;
using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Abilities
{
    public class ShootAbility:MonoBehaviour, IAbility
    {
        [SerializeField] private GameObject _bulletPrefab;
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
            Instantiate(_bulletPrefab, playerTransform.position+Vector3.up, playerTransform.rotation);
        }
    }
}