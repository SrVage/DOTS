using Unity.Entities;
using UnityEngine;
using Behaviour = Code.Behaviours.Behaviour;

namespace Code.Components
{
    public class BehaviourManager:MonoBehaviour, IConvertGameObjectToEntity
    {
        public Behaviour[] Behaviours;
        public Behaviour CurrentBehaviour;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new AIManager());
        }
    }

    public struct AIManager : IComponentData
    {
        
    }
}