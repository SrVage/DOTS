using System.Linq;
using Code.Behaviours;
using Code.Components;
using Unity.Entities;

namespace Code.Systems
{
    public class AIEvaluateSystem:ComponentSystem
    {
        private EntityQuery _evaluateQuery;
        
        protected override void OnCreate()
        {
            _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIManager>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_evaluateQuery).ForEach((Entity entity, BehaviourManager behaviourManager) =>
            {
                Behaviour behaviour = behaviourManager.Behaviours
                    .OrderBy(evalute => evalute.Evaluate())
                    .FirstOrDefault();
                behaviourManager.CurrentBehaviour = behaviour;
            });
        }
    }
}