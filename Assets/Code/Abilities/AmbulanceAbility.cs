using Code.Components.Interfaces;
using Photon.Pun;

namespace Code.Abilities
{
    public class AmbulanceAbility:DamageAbility
    {
        public override void Execute()
        {
            foreach (var collision in _collisions)
            {
                if (collision.TryGetComponent<ITakeDamage>(out var damage))
                {
                    damage.Damage(-_damage);
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }
}