using Code.Network;
using Unity.Entities;

namespace Code.Components.Interfaces
{
    public interface ITakeDamage
    {
        void Init(Entity entity);
        void NetworkInit(bool isLocal, SynchronizedParameters synchronizedParameters);
        void Damage(float damage);
    }
}