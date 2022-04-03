using Unity.Entities;
using Unity.Mathematics;

namespace Code.Components.Character
{
    public struct InputData:IComponentData
    {
        public float2 MoveDirection;
        public float Shoot;
        public float Jerk;
    }
}