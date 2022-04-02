using Code.Components;
using Code.Components.Interfaces;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SearchService;

namespace Code.Systems
{
    public class CharacterShootSystem:ComponentSystem
    {
        private EntityQuery _shootQuery;
        protected override void OnCreate()
        {
            _shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), 
                ComponentType.ReadOnly<ShootData>(),
                ComponentType.ReadOnly<UserInputData>()
                );
        }

        protected override void OnUpdate()
        {
            Entities.With(_shootQuery).ForEach((Entity entity, UserInputData userInputData, ref InputData inputData) =>
            {
                if (inputData.Shoot > 0f && userInputData.ShootAbility is IAbility shootAbility)
                    shootAbility.Execute();
            });
        }
    }
}