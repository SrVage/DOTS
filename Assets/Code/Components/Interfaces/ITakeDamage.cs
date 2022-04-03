using Unity.Entities;

namespace Code.Components.Interfaces
{
    public interface ITakeDamage
    {
        void Init(Entity entity);
        void Damage(float damage);
    }
}