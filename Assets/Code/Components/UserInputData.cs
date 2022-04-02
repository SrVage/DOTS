using Code.Components.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Components
{
    public class UserInputData:MonoBehaviour, IConvertGameObjectToEntity
    {
        public MonoBehaviour ShootAbility => _shootAbility;
        public MonoBehaviour JerkAbility => _jerkAbility;
        [SerializeField] private float _speed;
        [SerializeField] private MonoBehaviour _shootAbility;
        [SerializeField] private MonoBehaviour _jerkAbility;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new InputData());
            dstManager.AddComponentData(entity, new MoveData()
            {
                Speed = _speed/100
            });
            if (_shootAbility != null && _shootAbility is IAbility ability)
            {
                dstManager.AddComponentData(entity, new ShootData());
            }

            if (_jerkAbility != null && _jerkAbility is IAbility jerkAbility)
            {
                dstManager.AddComponentData(entity, new JerkData());
            }
        }
    }

    public struct InputData:IComponentData
    {
        public float2 MoveDirection;
        public float Shoot;
        public float Jerk;
    }

    public struct MoveData : IComponentData
    {
        public float Speed;
    }

    public struct ShootData : IComponentData
    {
    }

    public struct JerkData : IComponentData
    {
        
    }
}