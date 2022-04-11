using Code.Components;
using Unity.Entities;

namespace Code.Systems
{
    public class AIBehaveSystem:ComponentSystem
    {
        private EntityQuery _behaveQuery;
        
        protected override void OnCreate()
        {
            _behaveQuery = GetEntityQuery(ComponentType.ReadOnly<AIManager>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_behaveQuery).ForEach((Entity entity, BehaviourManager behaviourManager) =>
            {
                behaviourManager.CurrentBehaviour.Behave();
            });
        }
    }
}