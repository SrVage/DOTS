using System;
using Unity.Entities;

namespace Code.Components.Character
{
    [Serializable]
    public struct HealthData:IComponentData
    {
        public float Health;
        public float MaxHealth;

        public override string ToString()
        {
            return Health.ToString();
        }
    }
}