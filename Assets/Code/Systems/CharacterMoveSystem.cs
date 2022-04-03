using Code.Components;
using Code.Components.Character;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SearchService;

namespace Code.Systems
{
    public class CharacterMoveSystem:ComponentSystem
    {
        private EntityQuery _moveQuery;
        protected override void OnCreate()
        {
            _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), 
                ComponentType.ReadOnly<MoveData>(),
                ComponentType.ReadOnly<Transform>()
                );
        }

        protected override void OnUpdate()
        {
            Entities.With(_moveQuery).ForEach((Entity entity, Transform transform, ref InputData inputData, ref MoveData moveData) =>
            {
                Vector3 direction =
                    new Vector3(inputData.MoveDirection.x, 0, inputData.MoveDirection.y);
                if (direction.sqrMagnitude<0.01f)
                    return;
                ref var playerTransform = ref transform;
                ref var speed = ref moveData.Speed;
                playerTransform.position += direction*speed;
                playerTransform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            });
        }
    }
}