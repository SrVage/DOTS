using Code.Components;
using Unity.Entities;
using UnityEngine;

namespace Code.Systems
{
    public class BulletRotateSystem:ComponentSystem
    {
        private EntityQuery _rotateQuerry;
        private EntityManager _entityManager;

        protected override void OnCreate()
        {
            _rotateQuerry = GetEntityQuery(ComponentType.ReadOnly<BulletTag>(),
                ComponentType.ReadOnly<Rotate>(),
                ComponentType.ReadOnly<Transform>());
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        }

        protected override void OnUpdate()
        {
            Entities.With(_rotateQuerry).ForEach((Entity entity, Transform transform, ref BulletTag tag) =>
            {
                ref var bulletTransform = ref transform;
                bulletTransform.rotation *= new Quaternion(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f));
                _entityManager.RemoveComponent<Rotate>(entity);
            });
        }
    }
}