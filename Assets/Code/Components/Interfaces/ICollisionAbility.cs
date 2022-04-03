using Code.Abilities;

namespace Code.Components.Interfaces
{
    public interface ICollisionAbility:IAbility
    {
        void Init(CollisionAbility parent);
    }
}