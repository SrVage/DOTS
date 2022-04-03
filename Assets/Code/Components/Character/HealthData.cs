using Unity.Entities;

namespace Code.Components.Character
{
    public struct HealthData:IComponentData
    {
        public float Health;
        public float MaxHealth;
    }
}