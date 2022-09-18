using Code.Abilities;
using Code.Components;
using Code.Components.Character;
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
                ComponentType.ReadOnly<BulletTimer>(), ComponentType.Exclude<InPool>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_timerQuerry).ForEach((Entity entity, ref BulletTimer timer, BulletAbility bulletAbility) =>
            {
                ref var time = ref timer.Value;
                time -= Time.DeltaTime;
                if (time <= 0)
                {
                    //_entityManager.DestroyEntity(entity);
                    bulletAbility.ReturnToPool();
                }
            });
        }
    }
}