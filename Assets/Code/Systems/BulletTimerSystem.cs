using Code.Components;
using Unity.Entities;
using UnityEngine;

namespace Code.Systems
{
    public class BulletTimerSystem:ComponentSystem
    {
        private EntityQuery _timerQuerry;
        private EntityManager _entityManager;

        
        protected override void OnCreate()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _timerQuerry = GetEntityQuery(ComponentType.ReadOnly<Transform>(),
                ComponentType.ReadOnly<BulletTimer>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_timerQuerry).ForEach((Entity entity, Transform transform, ref BulletTimer timer) =>
            {
                ref var time = ref timer.Value;
                time -= Time.DeltaTime;
                if (time <= 0)
                {
                    _entityManager.DestroyEntity(entity);
                    Object.Destroy(transform.gameObject);
                }
            });
        }
    }
}