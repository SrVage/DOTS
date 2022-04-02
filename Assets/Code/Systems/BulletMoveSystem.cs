using Code.Components;
using Unity.Entities;
using UnityEngine;

namespace Code.Systems
{
    public class BulletMoveSystem:ComponentSystem
    {
        private EntityQuery _moveQuerry;
        
        protected override void OnCreate()
        {
            _moveQuerry = GetEntityQuery(ComponentType.ReadOnly<BulletTag>(),
                ComponentType.ReadOnly<Transform>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_moveQuerry).ForEach((Transform transform, ref BulletTag tag) =>
            {
                ref var bulletTransform = ref transform;
                bulletTransform.position += bulletTransform.forward * tag.Speed * Time.DeltaTime;
            });
        }
    }
}