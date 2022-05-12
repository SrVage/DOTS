using Unity.Entities;
using UnityEngine;

namespace Code.Components.Character
{
    public struct AnimationData : IComponentData
    {
        public int MoveHash;
        public int AttackHash;
    }
}