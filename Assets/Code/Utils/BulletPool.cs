using System.Collections.Generic;
using System.Linq;
using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Utils
{
    public sealed class BulletPool:IPool<GameObject>
    {
        private readonly List<GameObject> _bullets;
        private readonly GameObject _bulletPrefab;
        private readonly Transform _rootPool;

        public BulletPool(GameObject bulletPrefab)
        {
            _bullets = new List<GameObject>();
            _bulletPrefab = bulletPrefab;
            _rootPool = new GameObject("RootHouses").transform;
            //InstantiateNewBullet();
        }
        public GameObject GetObject()
        {
            var bullet = _bullets.FirstOrDefault(a => !a.activeSelf);
            if (bullet == null)
                return InstantiateNewBullet();
            return GetBulletFromPool();
        }

        private GameObject GetBulletFromPool()
        {
            var bullet = _bullets.First(a => !a.activeSelf);
            bullet.SetActive(true);
            bullet.transform.SetParent(null);
            return bullet;
        }

        private GameObject InstantiateNewBullet()
        {
            var instantiate = Object.Instantiate(_bulletPrefab);
            ReturnToPool(instantiate);
            _bullets.Add(instantiate);
            return GetObject();
        }

        public void ReturnToPool(GameObject bullet)
        {
            bullet.transform.SetParent(_rootPool);
            bullet.SetActive(false);
        }
    }
}