using Unity.Entities;
using UnityEngine;

namespace Code.Components
{
    public class BulletData:MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float _destroyTime;
        [SerializeField] private float _bulletSpeed;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new BulletTag()
            {
                Speed = _bulletSpeed
            });
            dstManager.AddComponentData(entity, new BulletTimer()
            {
                Value = _destroyTime
            });
        }
    }

    public struct BulletTag:IComponentData
    {
        public float Speed;
    }

    public struct BulletTimer:IComponentData
    {
        public float Value;
    }

    public struct Rotate : IComponentData
    {
        
    }

    public struct JumpData : IComponentData
    {
        
    }
}