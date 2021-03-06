using Code.Components.Character;
using Unity.Entities;

namespace Code.Systems
{
    public class TakeDamageSystem:ComponentSystem
    {
        private EntityQuery _damageQuery;
        private EntityManager _entityManager;
        
        protected override void OnCreate()
        {
            _damageQuery = GetEntityQuery(ComponentType.ReadOnly<Damage>(),
                ComponentType.ReadOnly<HealthData>());
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        protected override void OnUpdate()
        {
            Entities.With(_damageQuery).ForEach((Entity entity, ref HealthData healthData, ref Damage damage) =>
            {
                ref var health = ref healthData.Health;
                health -= damage.Value;
                _entityManager.RemoveComponent<Damage>(entity);
            });
        }
    }
}