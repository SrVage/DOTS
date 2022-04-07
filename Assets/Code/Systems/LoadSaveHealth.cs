using Code.Components.Character;
using Code.Tools;
using Unity.Entities;

namespace Code.Systems
{
    public class LoadSaveHealth:ComponentSystem
    {
        private EntityQuery _healthQuery;

        protected override void OnCreate()
        {
            _healthQuery = GetEntityQuery(ComponentType.ReadOnly<HealthData>());
            /*if (int.TryParse(GoogleDriveTool.Download("health").ToString(), out int hpResult))
            {
                Entities.With(_healthQuery).With(_healthQuery).ForEach((ref HealthData healthData) =>
                {
                    ref var health = ref healthData.Health;
                    health = hpResult;
                });
            }*/
        }

        protected override void OnUpdate()
        {
            
        }
    }
}